using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebApi.Model.RiotDtos.Summoner;
using WebApi.RiotApiClient.Misc;
using WebApi.RiotApiClient.Services.Interfaces;

namespace WebApi.RiotApiClient.Services
{
    public class SummonerService : ISummonerService
    {
        private readonly IWebService _webService;

        public SummonerService(IWebService webService)
        {
            _webService = webService;
        }

        public async Task<IDictionary<string, SummonerDto>> GetSummonersByNameAsync(Region region, IEnumerable<string> summonerNames)
        {
            var url = $"api/lol/{region}/v1.4/summoner/by-name/{string.Join(",", summonerNames)}";

            var response = await _webService.GetRequestAsync(region, url);

            return JsonConvert.DeserializeObject<Dictionary<string, SummonerDto>>(response);
        }
    }
}
