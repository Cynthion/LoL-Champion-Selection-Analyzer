using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebApi.Services.RiotApi.Interfaces;

namespace WebApi.Services.RiotApi
{
    public class RiotWebService : IWebService
    {
        private readonly RiotApiKey _riotApiKey;
        private readonly IRegionSelector _regionSelector;

        public RiotWebService(IRegionSelector regionSelector, RiotApiKey riotApiKey)
        {
            _riotApiKey = riotApiKey;
            _regionSelector = regionSelector;
        }

        public Task<string> GetRequestAsync(string url)
        {
            url.AddUrlParameter($"api_key={_riotApiKey.ApiKey}");
            var baseUrl = $"https://{_regionSelector.GetRegion().ToLower()}.api.pvp.net/{url}";     
            
            var client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            client.DefaultRequestHeaders.Add("User-Agent", "Champion Selection Analyzer");

            Console.WriteLine($"Calling { baseUrl }");
            var response = client.GetStringAsync(baseUrl);

            Console.WriteLine($"{ nameof(RiotWebService) } retrieved: { response }");
            return response;
        }
    }
}
