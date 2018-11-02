using System.Threading.Tasks;
using ChampionSelectionAnalyzer.RiotApiClient.Misc;
using ChampionSelectionAnalyzer.RiotApiClient.Services.Interfaces;
using ChampionSelectionAnalyzer.RiotModel.Summoner;
using Newtonsoft.Json;

namespace ChampionSelectionAnalyzer.RiotApiClient.Services
{
    public class SummonerService : ISummonerService
    {
        private readonly IWebService _webService;

        public SummonerService(IWebService webService)
        {
            _webService = webService;
        }

        public async Task<SummonerDto> GetSummonerByIdAsync(Region region, long summonerId)
        {
            var url = $"lol/summoners/v3/summoners/{summonerId}";

            var response = await _webService.GetRequestAsync(region, url);

            return JsonConvert.DeserializeObject<SummonerDto>(response);
        }
    }
}
