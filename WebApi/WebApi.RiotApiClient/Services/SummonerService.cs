using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebApi.Model.Enums;
using WebApi.Model.RiotDtos.Summoner;
using WebApi.RiotApiClient.Services.Interfaces;

namespace WebApi.RiotApiClient.Services
{
    public class SummonerService : ISummonerService
    {
        private readonly IRiotWebService _riotWebService;

        public SummonerService(IRiotWebService riotWebService)
        {
            _riotWebService = riotWebService;
        }

        public async Task<IDictionary<string, SummonerDto>> GetSummonersByNameAsync(Region region, IEnumerable<string> summonerNames)
        {
            var url = $"api/lol/{region}/v1.4/summoner/by-name/{string.Join(",", summonerNames)}";

            var response = await _riotWebService.GetRequestAsync(region, url);

            return JsonConvert.DeserializeObject<Dictionary<string, SummonerDto>>(response);
        }
    }
}
