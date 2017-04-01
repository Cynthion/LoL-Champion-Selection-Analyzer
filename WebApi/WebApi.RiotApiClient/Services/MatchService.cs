using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebApi.Model.Dtos.Match;
using WebApi.RiotApiClient.Misc;
using WebApi.RiotApiClient.Services.Interfaces;

namespace WebApi.RiotApiClient.Services
{
    public class MatchService : IMatchService
    {
        private readonly IWebService _webService;

        public MatchService(IWebService webService)
        {
            _webService = webService;
        }

        public async Task<MatchList> GetMatchListAsync(Region region, long summonerId, ICollection<string> rankedQueues, ICollection<string> seasons)
        {
            var url = $"/api/lol/{region}/v2.2/matchlist/by-summoner/{summonerId}";

            if (rankedQueues.Any())
            {
                url = url.AddUrlParameter($"rankedQueues={string.Join(",", rankedQueues)}");
            }

            if (seasons.Any())
            {
                url = url.AddUrlParameter($"seasons={string.Join(",", seasons)}");
            }

            var response = await _webService.GetRequestAsync(region, url);

            return JsonConvert.DeserializeObject<MatchList>(response);
        }

        public async Task<MatchDetailDto> GetMatchAsync(Region region, long matchId)
        {
            var url = $"/api/lol/{region}/v2.2/match/{matchId}";

            var response = await _webService.GetRequestAsync(region, url);

            return JsonConvert.DeserializeObject<MatchDetailDto>(response);
        }
    }
}
