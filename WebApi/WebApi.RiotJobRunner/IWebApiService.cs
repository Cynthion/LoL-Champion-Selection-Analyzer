using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Model.Dtos.League;
using WebApi.Model.Dtos.Match;

namespace WebApi.RiotJobRunner
{
    internal interface IWebApiService
    {
        Task<long> GetLeagueEntryCountAsync();

        Task<long> GetMatchReferenceCountAsync();

        Task<IEnumerable<LeagueEntry>> GetLeagueEntriesAsync();

        Task<IEnumerable<MatchReference>> GetMatchReferencesAsync();

        Task PostLeagueAsync(League league);

        Task PostMatchlistAsync(MatchList league);
    }
}
