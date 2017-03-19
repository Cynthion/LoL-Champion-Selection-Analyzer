namespace WebApi.Models.Dtos.Stats
{
    // Dominion data removed
    public class AggregatedStatsDto
    {
        public int TotalPhysicalDamageDealt { get; set; }
        public int TotalTurretsKilled { get; set; }
        public int TotalSessionsPlayed { get; set; }
        public int TotalAssists { get; set; }
        public int TotalDamageDealt { get; set; }
        public int TotalQuadraKills { get; set; }
        public int KillingSpree { get; set; }
        public int MostSpellsCast { get; set; }
        public int TotalDoubleKills { get; set; }
        public int MaxChampionsKilled { get; set; }
        public int TotalDeathsPerSession { get; set; } //Only returned for ranked statistics.
        public int TotalSessionsWon { get; set; }
        public int MaxTimeSpentLiving { get; set; }
        public int TotalPentaKills { get; set; }
        public int TotalTripleKills { get; set; }
        public int TotalNeutralMinionsKilled { get; set; }
        public int TotalGoldEarned { get; set; }
        public int RankedPremadeGamesPlayed { get; set; }
        public int RankedSoloGamesPlayed { get; set; }
        public int MaxLargestKillingSpree { get; set; }
        public int TotalChampionKills { get; set; }
        public int MaxNumDeaths { get; set; } //Only returned for ranked statistics.
        public int TotalDamageTaken { get; set; }
        public int TotalMinionKills { get; set; }
        public int TotalMagicDamageDealt { get; set; }
        public int TotalHeal { get; set; }
        public int NormalGamesPlayed { get; set; }
        public int MostChampionKillsPerSession { get; set; }
        public int TotalUnrealKills { get; set; }
        public int MaxTimePlayed { get; set; }
        public int MaxLargestCriticalStrike { get; set; }
        public int BotGamesPlayed { get; set; }
        public int TotalSessionsLost { get; set; }
        public int TotalFirstBlood { get; set; }
    }
}
