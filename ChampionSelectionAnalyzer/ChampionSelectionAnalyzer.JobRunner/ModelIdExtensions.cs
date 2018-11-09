using ChampionSelectionAnalyzer.RiotModel.League;
using ChampionSelectionAnalyzer.RiotModel.Summoner;

namespace ChampionSelectionAnalyzer.JobRunner
{
    internal static class ModelIdExtensions
    {
        public static string Id(this LeagueListDto leagueListDto)
        {
            return $"{nameof(LeagueListDto)}/{leagueListDto.Region}/{leagueListDto.Tier}";
        }

        public static string Id(this SummonerDto summonerDto)
        {
            return $"{nameof(SummonerDto)}/{summonerDto.SummonerId}";
        }
    }
}
