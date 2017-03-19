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

        public WebService(RiotApiKey riotApiKey)
        {
            _riotApiKey = riotApiKey;
        }

        public Task<string> GetRequestAsync(string url)
        {
            url = AddRiotApiKey(url);
            
            Console.WriteLine($"Calling { url }");

            var client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            client.DefaultRequestHeaders.Add("User-Agent", "Champion Selection Analyzer");

            return client.GetStringAsync(url);
        }

        private string AddRiotApiKey(string url)
        {
            return string.Format("{0}?api_key={1}", url, _riotApiKey.ApiKey);
        }
    }
}
