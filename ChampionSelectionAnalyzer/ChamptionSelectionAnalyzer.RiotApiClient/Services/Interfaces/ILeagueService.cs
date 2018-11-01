using System.Threading.Tasks;
using ChampionSelectionAnalyzer.RiotApiClient.Misc;
using ChampionSelectionAnalyzer.RiotModel.League;

namespace ChampionSelectionAnalyzer.RiotApiClient.Services.Interfaces
{
    public interface ILeagueService
    {
        Task<LeagueListDto> GetChallengerLeaguesByQueueAsync(Region region, QueueType queueType);

        Task<LeagueListDto> GetMasterLeaguesByQueueAsync(Region region, QueueType queueType);
    }
}
