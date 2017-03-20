using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Misc;
using WebApi.Models.Dtos.Stats;
using WebApi.Services.Interfaces;

namespace WebApi.Services
{
    public class StatsService : IStatsService
    {
        private readonly IWebService _webService;

        public StatsService(IWebService webService)
        {
            _webService = webService;
        }

        // TODO by Season
        public async Task<IEnumerable<RankedStatsDto>> GetRankedStatsBySummoner(Region region, string summonerId, Season season)
        {
            var url = $"api/lol/{region}/v1.3/stats/by-summoner/{summonerId}/ranked";

            var response = await _webService.GetRequestAsync(url);

            Console.WriteLine($"{ nameof(StatsService) } retrieved: { response }");

            return JsonConvert.DeserializeObject<RankedStatsDto[]>(response);
        }
    }
}
