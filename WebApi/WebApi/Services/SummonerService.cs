using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebApi.Models;
using WebApi.Models.Dtos.Summoner;
using WebApi.Services.Interfaces;

namespace WebApi.Services
{
    public class SummonerService : ISummonerService
    {
        private readonly IWebService _webService;
        private readonly IRegionSelector _regionSelector;

        public SummonerService(IWebService webService, IRegionSelector regionSelector)
        {
            _webService = webService;
            _regionSelector = regionSelector;
        }

        public async Task<IDictionary<string, SummonerDto>> GetSummonersByNameAsync(IEnumerable<string> summonerNames)
        {
            var dictionary = new Dictionary<string, SummonerDto>();
            var url = string.Format(
                "https://{0}.api.pvp.net/api/lol/EUW/v1.4/summoner/by-name/{1}",
                _regionSelector.GetRegion().ToLower(),
                string.Join(", ", summonerNames));

            var response = await _webService.GetRequestAsync(url);

            Console.WriteLine($"{ nameof(SummonerService) } retrieved: { response }");

            return JsonConvert.DeserializeObject<Dictionary<string, SummonerDto>>(response);
        }
    }
}
