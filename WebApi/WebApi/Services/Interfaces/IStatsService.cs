using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Misc;
using WebApi.Models.Dtos.Stats;

namespace WebApi.Services.Interfaces
{
    public interface IStatsService
    {
        Task<IEnumerable<RankedStatsDto>> GetRankedStatsBySummoner(Region region, string summonerId, Season season);
    }
}
