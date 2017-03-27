using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebApi.Misc;
using WebApi.Misc.Exceptions;
using WebApi.Misc.Interfaces;
using WebApi.Services.RiotApi.Interfaces;

namespace WebApi.Services.RiotApi
{
    // TODO for region-independant calls, allow region-swapping with IRegionSelector
    // Riot API Information:
    // HTTP Status Codes: https://developer.riotgames.com/response-code.html

    public class RiotWebService : IWebService
    {
        private readonly IApiKey _riotApiKey;
        private readonly IDictionary<Region, IRateLimitEnforcer> _rateLimitEnforcers;

        private readonly HttpClient _httpClient;

        private const string XRateLimitTypeHeader = "X-Rate-Limit-Type";
        private const string XAppRateLimitCountHeader = "X-App-Rate-Limit-Count";
        private const string XMethodRateLimitCountHeader = "X-Method-Rate-Limit-Count";
        private const string RetryAfterHeader = "Retry-After";

        public RiotWebService(IApiKey riotApiKey)
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
            Console.WriteLine($"Calling {baseUrl}");

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

            Console.WriteLine($"{nameof(RiotWebService)} retrieved: {result}");
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
                    case HttpStatusCode.ServiceUnavailable:
                        throw new ChampionSelectionAnalyzerException("503, Service unavailable", statusCode);
                    case HttpStatusCode.InternalServerError:
                        throw new ChampionSelectionAnalyzerException("500, Internal server error", statusCode);
                    case HttpStatusCode.Unauthorized:
                        throw new ChampionSelectionAnalyzerException("401, Unauthorized", statusCode);
                    case HttpStatusCode.BadRequest:
                        throw new ChampionSelectionAnalyzerException("400, Bad request", statusCode);
                    case HttpStatusCode.NotFound:
                        throw new ChampionSelectionAnalyzerException("404, Resource not found", statusCode);
                    case HttpStatusCode.Forbidden:
                        throw new ChampionSelectionAnalyzerException("403, Forbidden", statusCode);
                }
            }
        }

        private void HandleRateLimit(HttpResponseMessage response, Region region)
        {
            Console.WriteLine("Rate limit detected:");

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
                    throw new ChampionSelectionAnalyzerException("Unsuccessful extraction of retry after delay.");
                }
            }
        }

        private static void ReadHeader(string headerName, HttpHeaders headers, out IEnumerable<string> headerValues)
        {
            headers.TryGetValues(headerName, out headerValues);
            Console.WriteLine($"{headerName}: {headerValues}");
        }
    }
}
