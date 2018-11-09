using ChampionSelectionAnalyzer.RiotModel.League;

namespace ChampionSelectionAnalyzer.JobRunner
{
    internal static class ModelIdExtensions
    {
        public static string Id(this LeagueListDto leagueListDto)
        {
            return $"{nameof(LeagueListDto)}/{leagueListDto.Region}/{leagueListDto.Tier}";
        }
    }
}
