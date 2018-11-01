using System.Threading.Tasks;
using ChampionSelectionAnalyzer.RiotApiClient.Misc;
using ChampionSelectionAnalyzer.RiotApiClient.Services.Interfaces;
using ChampionSelectionAnalyzer.RiotModel.League;
using Newtonsoft.Json;

namespace ChampionSelectionAnalyzer.RiotApiClient.Services
{
    public class LeagueService : ILeagueService
    {
        private readonly IWebService _webService;

        public LeagueService(IWebService webService)
        {
            _webService = webService;
        }

        public async Task<LeagueListDto> GetChallengerLeaguesByQueueAsync(Region region, QueueType queueType)
        {
            var url = $"lol/league/v3/challengerleagues/by-queue/{queueType}";

            var response = await _webService.GetRequestAsync(region, url);

            return JsonConvert.DeserializeObject<LeagueListDto>(response);
        }

        public async Task<LeagueListDto> GetMasterLeaguesByQueueAsync(Region region, QueueType queueType)
        {
            var url = $"lol/league/v3/masterleagues/by-queue/{queueType}";

            var response = await _webService.GetRequestAsync(region, url);

            return JsonConvert.DeserializeObject<LeagueListDto>(response);
        }
    }
}
