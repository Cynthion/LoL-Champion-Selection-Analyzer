using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebApi.Services.Interfaces;

namespace WebApi.Services
{
    public class WebService : IWebService
    {
        private readonly RiotApiKey _riotApiKey;
        private readonly IRegionSelector _regionSelector;

        public WebService(IRegionSelector regionSelector, RiotApiKey riotApiKey)
        {
            _riotApiKey = riotApiKey;
            _regionSelector = regionSelector;
        }

        public Task<string> GetRequestAsync(string url)
        {
            url = AddRiotApiKey(url);
            var baseUrl = $"https://{_regionSelector.GetRegion().ToLower()}.api.pvp.net/{url}";     
            
            var client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            client.DefaultRequestHeaders.Add("User-Agent", "Champion Selection Analyzer");

            Console.WriteLine($"Calling { baseUrl }");
            var response = client.GetStringAsync(baseUrl);

            Console.WriteLine($"{ nameof(WebService) } retrieved: { response }");
            return response;
        }

        private string AddRiotApiKey(string url)
        {
            return string.Format("{0}?api_key={1}", url, _riotApiKey.ApiKey);
        }
    }
}
