using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebApi.Model.Enums;
using WebApi.Model.RiotDtos.Stats;
using WebApi.RiotApiClient.Services.Interfaces;

namespace WebApi.RiotApiClient.Services
{
    public class StatsService : IStatsService
    {
        private readonly IRiotWebService _riotWebService;

        public StatsService(IRiotWebService riotWebService)
        {
            _riotWebService = riotWebService;
        }

        // TODO by Season
        public async Task<IEnumerable<RankedStatsDto>> GetRankedStatsBySummoner(Region region, string summonerId, string season)
        {
            var url = $"api/lol/{region}/v1.3/stats/by-summoner/{summonerId}/ranked";

            if (!string.IsNullOrEmpty(season))
            {
                url = url.AddUrlParameter($"season={season}");
            }

            var response = await _riotWebService.GetRequestAsync(region, url);

            return JsonConvert.DeserializeObject<RankedStatsDto[]>(response);
        }
    }
}
