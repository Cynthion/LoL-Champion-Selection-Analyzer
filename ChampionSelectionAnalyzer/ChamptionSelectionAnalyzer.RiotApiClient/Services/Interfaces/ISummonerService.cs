using System.Threading.Tasks;
using ChampionSelectionAnalyzer.RiotApiClient.Misc;
using ChampionSelectionAnalyzer.RiotModel.Summoner;

namespace ChampionSelectionAnalyzer.RiotApiClient.Services.Interfaces
{
    public interface ISummonerService
    {
        Task<SummonerDto> GetSummonerByIdAsync(Region region, long summonerId);
    }
}
