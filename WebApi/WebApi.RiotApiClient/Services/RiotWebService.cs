using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using NLog;
using WebApi.RiotApiClient.Misc;
using WebApi.RiotApiClient.Misc.Exceptions;
using WebApi.RiotApiClient.Misc.Interfaces;
using WebApi.RiotApiClient.Services.Interfaces;

namespace WebApi.RiotApiClient.Services
{
    // TODO for region-independant calls, allow region-swapping with IRegionSelector
    // Riot API Information:
    // HTTP Status Codes: https://developer.riotgames.com/response-code.html

    /// <summary>
    /// This class is implemented as Singleton in order to ensure the Riot API Rate Limitation.
    /// </summary>
    public sealed class RiotWebService : IWebService
    {
        // Singleton implemented according to https://msdn.microsoft.com/en-us/library/ff650316.aspx
        public static RiotWebService Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }
                lock (SyncRoot)
                {
                    if (_instance == null)
                    {
                        _instance = new RiotWebService(RiotApiKey.CreateFromFile());
                    }
                }

                return _instance;
            }
        }

        private static volatile RiotWebService _instance;
        private static readonly object SyncRoot = new object();
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        private readonly IApiKey _riotApiKey;
        private readonly IDictionary<Region, IRateLimitEnforcer> _rateLimitEnforcers;

        private readonly HttpClient _httpClient;

        private const string XRateLimitTypeHeader = "X-Rate-Limit-Type";
        private const string XAppRateLimitCountHeader = "X-App-Rate-Limit-Count";
        private const string XMethodRateLimitCountHeader = "X-Method-Rate-Limit-Count";
        private const string RetryAfterHeader = "Retry-After";

        private RiotWebService(IApiKey riotApiKey)
        {
            _riotApiKey = riotApiKey;
            _rateLimitEnforcers = new Dictionary<Region, IRateLimitEnforcer>();

            _httpClient = new HttpClient();
        }

        public async Task<string> GetRequestAsync(Region region, string url)
        {
            string result;

            url = url.AddUrlParameter($"api_key={_riotApiKey.ApiKey}");
            var baseUrl = $"https://{region.ToString().ToLower()}.api.pvp.net/{url}";
            Logger.Debug($"Calling {baseUrl}");

            PrepareHttpClient();

            await GetRateLimitEnforcer(region).EnforceRateLimitAsync();

            using (var response = await _httpClient.GetAsync(baseUrl))
            {
                if (!response.IsSuccessStatusCode)
                {
                    HandleRequestFailure(response, region);
                }
                using (var content = response.Content)
                {
                    result = await content.ReadAsStringAsync();
                }
            }

            Logger.Trace($"{nameof(RiotWebService)} retrieved: {result}");
            return result;
        }

        private void PrepareHttpClient()
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Champion Selection Analyzer");
        }

        private IRateLimitEnforcer GetRateLimitEnforcer(Region region)
        {
            if (!_rateLimitEnforcers.ContainsKey(region))
            {
                _rateLimitEnforcers.Add(region, new RateLimitEnforcer(_riotApiKey));
            }
            return _rateLimitEnforcers[region];
        }

        private void HandleRequestFailure(HttpResponseMessage response, Region region)
        {
            var statusCode = response.StatusCode;

            if ((int)statusCode == 429)
            {
                HandleRateLimit(response, region);
            }
            else
            {
                switch (statusCode)
                {
                    case HttpStatusCode.BadRequest:
                        throw new RioApiException("400, Bad Request", statusCode);
                    case HttpStatusCode.Unauthorized:
                        throw new RioApiException("401, Unauthorized", statusCode);
                    case HttpStatusCode.Forbidden:
                        throw new RioApiException("403, Forbidden", statusCode);
                    case HttpStatusCode.NotFound:
                        throw new RioApiException("404, Not Found", statusCode);
                    case HttpStatusCode.InternalServerError:
                        throw new RioApiException("500, Internal Server Error", statusCode);
                    case HttpStatusCode.ServiceUnavailable:
                        throw new RioApiException("503, Service Unavailable", statusCode);
                    default:
                        throw new RioApiException("Unsuccessful HttpStatusCode", statusCode);
                }
            }
        }

        private void HandleRateLimit(HttpResponseMessage response, Region region)
        {
            Logger.Warn("Rate limit detected:");

            ReadHeader(XRateLimitTypeHeader, response.Headers, out IEnumerable<string> headerValues);
            ReadHeader(XAppRateLimitCountHeader, response.Headers, out headerValues);
            ReadHeader(XMethodRateLimitCountHeader, response.Headers, out headerValues);
            ReadHeader(RetryAfterHeader, response.Headers, out headerValues);

            if (headerValues.Any())
            {
                if (int.TryParse(headerValues.First(), out int retryAfterContent))
                {
                    var retryAfterDelay = TimeSpan.FromSeconds(retryAfterContent);
                    GetRateLimitEnforcer(region).SetRetryAfter(retryAfterDelay);
                }
                else
                {
                    throw new RioApiException("Unsuccessful extraction of retry after delay.");
                }
            }
        }

        private static void ReadHeader(string headerName, HttpHeaders headers, out IEnumerable<string> headerValues)
        {
            headers.TryGetValues(headerName, out headerValues);
            Logger.Warn($"{headerName}: {string.Join("\n", headerValues)}");
        }
    }
}
