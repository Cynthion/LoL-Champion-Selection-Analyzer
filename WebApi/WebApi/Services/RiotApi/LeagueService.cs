using System.Threading.Tasks;
using Newtonsoft.Json;
using WebApi.Misc;
using WebApi.Models.Dtos.League;
using WebApi.Services.RiotApi.Interfaces;

namespace WebApi.Services.RiotApi
{
    public class LeagueService : ILeagueService
    {
        private readonly IWebService _webService;

        public LeagueService(IWebService webService)
        {
            _webService = webService;
        }

        public async Task<LeagueDto> GetMasterTierLeagues(Region region, string queueType)
        {
            var url = $"api/lol/{region}/v2.5/league/master?type={queueType}";

            var response = await _webService.GetRequestAsync(url);

            return JsonConvert.DeserializeObject<LeagueDto>(response);
        }

        public async Task<LeagueDto> GetChallengerTierLeagues(Region region, string queueType)
        {
            var url = $"api/lol/{region}/v2.5/league/challenger?type={queueType}";

            var response = await _webService.GetRequestAsync(url);

            return JsonConvert.DeserializeObject<LeagueDto>(response);
        }
    }
}
