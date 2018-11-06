using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ChampionSelectionAnalyzer.RiotApiClient.Misc;
using ChampionSelectionAnalyzer.RiotApiClient.Misc.Interfaces;
using ChampionSelectionAnalyzer.RiotApiClient.Services.Interfaces;
using NLog;

namespace ChampionSelectionAnalyzer.RiotApiClient.Services
{
    public sealed class RiotWebService : IWebService
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        private readonly IApiKey _riotApiKey;

        private readonly HttpClient _httpClient;

        private const string XRateLimitTypeHeader = "X-Rate-Limit-Type";
        private const string XAppRateLimitCountHeader = "X-App-Rate-Limit-Count";
        private const string XMethodRateLimitCountHeader = "X-Method-Rate-Limit-Count";
        private const string RetryAfterHeader = "Retry-After";

        public RiotWebService(IApiKey riotApiKey)
        {
            _riotApiKey = riotApiKey;
            _httpClient = new HttpClient();
        }

        public async Task<string> GetRequestAsync(Region region, string url)
        {
            string result;

            url = url.AddUrlParameter($"api_key={_riotApiKey.ApiKey}");
            var requestUrl = $"https://{region.GetRegionEndpoint()}/{url}";
            Logger.Log(LogLevel.Debug, $"Calling {requestUrl}");

            PrepareHttpHeaders();

            var rateLimitEnforcer = RateLimitEnforcer.GetRateLimitEnforcer(_riotApiKey, region);
            await rateLimitEnforcer.EnforceRateLimitAsync();

            using (var response = await _httpClient.GetAsync(requestUrl))
            {
                if (!response.IsSuccessStatusCode)
                {
                    HandleRequestFailure(response, rateLimitEnforcer);
                }
                using (var content = response.Content)
                {
                    result = await content.ReadAsStringAsync();
                }
            }

            Logger.Trace($"{nameof(RiotWebService)} retrieved: {result}");
            return result;
        }

        private void PrepareHttpHeaders()
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Champion Selection Analyzer");
        }

        private void HandleRequestFailure(HttpResponseMessage response, IRateLimitEnforcer rateLimitEnforcer)
        {
            var statusCode = response.StatusCode;

            if ((int)statusCode == 429)
            {
                HandleRateLimit(response, rateLimitEnforcer);
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

        // TODO from response headers: read application, method, service limits and counts, and adapt
        private void HandleRateLimit(HttpResponseMessage response, IRateLimitEnforcer rateLimitEnforcer)
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
                    rateLimitEnforcer.SetRetryAfter(retryAfterDelay);
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
