using System.Threading.Tasks;
using Newtonsoft.Json;
using WebApi.Model.Dtos.League;
using WebApi.RiotApiClient.Misc;
using WebApi.RiotApiClient.Services.Interfaces;

namespace WebApi.RiotApiClient.Services
{
    public class LeagueService : ILeagueService
    {
        private readonly IWebService _webService;

        public LeagueService(IWebService webService)
        {
            _webService = webService;
        }

        public async Task<LeagueDto> GetMasterTierLeaguesAsync(Region region, string queueType)
        {
            var url = $"api/lol/{region}/v2.5/league/master?type={queueType}";

            var response = await _webService.GetRequestAsync(region, url);

            return JsonConvert.DeserializeObject<LeagueDto>(response);
        }

        public async Task<LeagueDto> GetChallengerTierLeaguesAsync(Region region, string queueType)
        {
            var url = $"api/lol/{region}/v2.5/league/challenger?type={queueType}";

            var response = await _webService.GetRequestAsync(region, url);

            return JsonConvert.DeserializeObject<LeagueDto>(response);
        }
    }
}
