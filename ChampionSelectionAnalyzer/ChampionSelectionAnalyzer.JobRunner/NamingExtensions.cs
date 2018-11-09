using ChampionSelectionAnalyzer.RiotModel.League;
using ChampionSelectionAnalyzer.RiotModel.Summoner;

namespace ChampionSelectionAnalyzer.JobRunner
{
    internal static class NamingExtensions
    {
        public static string Name(this LeagueListDto leagueListDto)
        {
            return $"LeagueList<" +
                   $"{nameof(leagueListDto.Region)}: {leagueListDto.Region}, " +
                   $"{nameof(leagueListDto.Tier)}: {leagueListDto.Tier}, " +
                   $"{nameof(leagueListDto.Queue)}: {leagueListDto.Queue}>";
        }

        public static string Name(this SummonerDto summonerDto)
        {
            return "Summoner<" +
                   $"{nameof(summonerDto.Name)}: {summonerDto.Name}, " +
                   $"{nameof(summonerDto.SummonerId)}: {summonerDto.SummonerId}, " +
                   $"{nameof(summonerDto.AccountId)}: {summonerDto.AccountId}>";
        }
    }
}
