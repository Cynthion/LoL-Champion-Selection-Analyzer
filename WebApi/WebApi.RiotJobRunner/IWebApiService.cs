using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Model.RiotDtos.League;
using WebApi.Model.RiotDtos.Match;
using WebApi.Model.RiotDtos.Matchlist;

namespace WebApi.RiotJobRunner
{
    internal interface IWebApiService
    {
        Task<long> GetLeagueEntryCountAsync();

        Task<long> GetMatchReferenceCountAsync();

        Task<IEnumerable<LeagueEntryDto>> GetLeagueEntriesAsync();

        Task<IEnumerable<MatchReferenceDto>> GetMatchReferencesAsync();

        Task PostLeagueAsync(LeagueDto leagueDto);

        Task PostMatchlistAsync(MatchListDto league);
    }
}
