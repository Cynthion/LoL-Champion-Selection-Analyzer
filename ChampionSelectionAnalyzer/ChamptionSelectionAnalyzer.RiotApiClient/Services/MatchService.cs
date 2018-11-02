using System;
using System.Threading.Tasks;
using ChampionSelectionAnalyzer.RiotApiClient.Misc;
using ChampionSelectionAnalyzer.RiotApiClient.Services.Interfaces;
using ChampionSelectionAnalyzer.RiotModel.Match;
using Newtonsoft.Json;

namespace ChampionSelectionAnalyzer.RiotApiClient.Services
{
    public class MatchService : IMatchService
    {
        private readonly IWebService _webService;

        public MatchService(IWebService webService)
        {
            _webService = webService;
        }

        public async Task<MatchListDto> GetMatchListByAccountAsync(Region region, long accountId, DateTime beginTime, DateTime endTime)
        {
            var url = $"lol/match/v3/matchlists/by-account/{accountId}";

            var response = await _webService.GetRequestAsync(region, url);

            return JsonConvert.DeserializeObject<MatchListDto>(response);
        }
    }
}
