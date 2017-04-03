using System.Threading.Tasks;
using Newtonsoft.Json;
using WebApi.Model.Enums;
using WebApi.Model.RiotDtos.League;
using WebApi.RiotApiClient.Services.Interfaces;

namespace WebApi.RiotApiClient.Services
{
    public class LeagueService : ILeagueService
    {
        private readonly IRiotWebService _riotWebService;

        public LeagueService(IRiotWebService riotWebService)
        {
            _riotWebService = riotWebService;
        }

        public async Task<LeagueDto> GetMasterTierLeaguesAsync(Region region, string queueType)
        {
            var url = $"api/lol/{region}/v2.5/league/master?type={queueType}";

            var response = await _riotWebService.GetRequestAsync(region, url);

            return JsonConvert.DeserializeObject<LeagueDto>(response);
        }

        public async Task<LeagueDto> GetChallengerTierLeaguesAsync(Region region, string queueType)
        {
            var url = $"api/lol/{region}/v2.5/league/challenger?type={queueType}";

            var response = await _riotWebService.GetRequestAsync(region, url);

            return JsonConvert.DeserializeObject<LeagueDto>(response);
        }
    }
}
