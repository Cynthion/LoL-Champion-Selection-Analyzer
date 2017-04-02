namespace WebApi.Model.RiotDtos.Match
{
    /// <summary>
    /// Unnecessary properties not contained.
    /// </summary>
    public class ParticipantStatsDto
    {
        public bool Winner { get; set; }

        public bool FirstBloodKill { get; set; }
        public bool FirstBloodAssist { get; set; }

        public bool FirstTowerKill { get; set; }
        public bool FirstTowerAssist { get; set; }

        public bool FirstInhibitorKill { get; set; }
        public bool FirstInhibitorAssist { get; set; }

        public long ChampLevel { get; set; }

        public long Item0 { get; set; }
        public long Item1 { get; set; }
        public long Item2 { get; set; }
        public long Item3 { get; set; }
        public long Item4 { get; set; }
        public long Item5 { get; set; }
        public long Item6 { get; set; }

        public long UnrealKills { get; set; }
        public long WardsKilled { get; set; }
        public long InhibitorKills { get; set; }
        public long TowerKills { get; set; }
        public long MinionsKilled { get; set; }
        public long NeutralMinionsKilled { get; set; }
        public long NeutralMinionsKilledTeamJungle { get; set; }

        public long Kills { get; set; }
        public long DoubleKills { get; set; }
        public long TripleKills { get; set; }
        public long QuadraKills { get; set; }
        public long PentaKills { get; set; }
        public long KillingSprees { get; set; }
        public long LargestKillingSpree { get; set; }
        public long LargestMultiKill { get; set; }
        public long LargestCriticalStrike { get; set; }

        public long Assists { get; set; }
        public long Deaths { get; set; }

        public long MagicDamageTaken { get; set; }
        public long MagicDamageDealt { get; set; }
        public long MagicDamageDealtToChampions { get; set; }
        public long PhysicalDamageTaken { get; set; }
        public long PhysicalDamageDealt { get; set; }
        public long PhysicalDamageDealtToChampions { get; set; }
        public long TrueDamageTaken { get; set; }
        public long TrueDamageDealt { get; set; }
        public long TrueDamageDealtToChampions { get; set; }
        public long TotalDamageTaken { get; set; }
        public long TotalDamageDealt { get; set; }
        public long TotalDamageDealtToChampions { get; set; }
        public long TotalHeal { get; set; }
        public long TotalUnitsHealed { get; set; }
        public long TotalTimeCrowdControlDealt { get; set; }

        public long GoldEarned { get; set; }
        public long GoldSpent { get; set; }

        public long WardsPlaced { get; set; }
        public long SightWardsBoughtInGame { get; set; }
        public long VisionWardsBoughtInGame { get; set; }
    }
}
