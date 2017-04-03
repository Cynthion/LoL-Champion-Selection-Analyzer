using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Model.Entities;
using WebApi.Model.Enums;
using WebApi.Model.RiotDtos.League;
using WebApi.Model.RiotDtos.Matchlist;

namespace WebApi.RiotJobRunner
{
    internal interface IWebApiService
    {
        Task<long> GetSummonerLeagueEntryCountAsync();

        Task<long> GetMatchReferenceCountAsync();

        Task<IEnumerable<SummonerLeagueEntry>> GetSummonerLeagueEntriesAsync();

        Task<IEnumerable<MatchReferenceDto>> GetMatchReferencesAsync();

        Task PostLeagueAsync(Region region, LeagueDto leagueDto);

        Task PostMatchlistAsync(Region region, MatchListDto league);
    }
}
