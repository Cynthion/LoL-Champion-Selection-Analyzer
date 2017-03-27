using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.RiotApiClient.Dtos.Stats;
using WebApi.RiotApiClient.Misc;

namespace WebApi.RiotApiClient.Services.Interfaces
{
    public interface IStatsService
    {
        Task<IEnumerable<RankedStatsDto>> GetRankedStatsBySummoner(Region region, string summonerId, string season);
    }
}
