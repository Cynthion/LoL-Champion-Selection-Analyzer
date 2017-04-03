using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Model.Enums;
using WebApi.Model.RiotDtos.Stats;

namespace WebApi.RiotApiClient.Services.Interfaces
{
    public interface IStatsService
    {
        Task<IEnumerable<RankedStatsDto>> GetRankedStatsBySummoner(Region region, string summonerId, string season);
    }
}
