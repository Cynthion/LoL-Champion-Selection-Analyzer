using System;
using System.Threading.Tasks;
using ChampionSelectionAnalyzer.RiotApiClient.Misc;
using ChampionSelectionAnalyzer.RiotModel.Match;

namespace ChampionSelectionAnalyzer.RiotApiClient.Services.Interfaces
{
    public interface IMatchService
    {
        Task<MatchListDto> GetMatchListByAccountAsync(Region region, long accountId, DateTime beginTime, DateTime endTime);
    }
}
