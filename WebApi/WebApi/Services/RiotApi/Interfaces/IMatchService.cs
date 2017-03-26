using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Misc;
using WebApi.Models.Dtos.Match;

namespace WebApi.Services.RiotApi.Interfaces
{
    public interface IMatchService
    {
        Task<MatchListDto> GetMatchListAsync(
            Region region, 
            long summonerId,
            ICollection<string> rankedQueues,
            ICollection<string> seasons);

        Task<MatchDetailDto> GetMatchAsync(Region region, long matchId);
    }
}
