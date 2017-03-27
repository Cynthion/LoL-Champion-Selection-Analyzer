using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebApi.RiotApiClient.Dtos.Stats;
using WebApi.RiotApiClient.Misc;
using WebApi.RiotApiClient.Services.Interfaces;

namespace WebApi.RiotApiClient.Services
{
    public class StatsService : IStatsService
    {
        private readonly IWebService _webService;

        public StatsService(IWebService webService)
        {
            _webService = webService;
        }

        // TODO by Season
        public async Task<IEnumerable<RankedStatsDto>> GetRankedStatsBySummoner(Region region, string summonerId, string season)
        {
            var url = $"api/lol/{region}/v1.3/stats/by-summoner/{summonerId}/ranked";

            if (!string.IsNullOrEmpty(season))
            {
                url = url.AddUrlParameter($"season={season}");
            }

            var response = await _webService.GetRequestAsync(region, url);

            return JsonConvert.DeserializeObject<RankedStatsDto[]>(response);
        }
    }
}
