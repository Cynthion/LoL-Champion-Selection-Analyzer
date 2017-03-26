using System;
using System.Collections.Generic;
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
                    HandleRequestFailure(response.StatusCode);
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

        private void HandleRequestFailure(HttpStatusCode statusCode)
        {
            var code = (int) statusCode;
            if (code == 429)
            {
                // TODO read headers, set RetryAfter delay to RateLimitEnforcer
            }

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
}
