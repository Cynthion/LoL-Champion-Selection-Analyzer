using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Model.RiotDtos.Match;
using WebApi.Model.RiotDtos.Matchlist;
using WebApi.RiotApiClient.Misc;

namespace WebApi.RiotApiClient.Services.Interfaces
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
