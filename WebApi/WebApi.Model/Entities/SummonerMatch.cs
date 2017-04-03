using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Model.Entities
{
    public class SummonerMatchlist
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SummonerId { get; set; }

        public int TotalGames { get; set; }

        public ICollection<SummonerMatch> Matches { get; set; }
    }

    public class SummonerMatch
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Key { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long MatchDetailId { get; set; }

        public long Timestamp { get; set; }

        public long Champion { get; set; }

        public string Season { get; set; }

        public string Region { get; set; }

        public string Queue { get; set; }

        public string Lane { get; set; }

        public string Role { get; set; }

        public MatchDetail MatchDetail { get; set; }
    }

    public class MatchDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long MatchId { get; set; }

        public long MatchCreation { get; set; }

        public long MatchDuration { get; set; }

        public string Season { get; set; }

        public string Region { get; set; }

        public string QueueType { get; set; }

        public ICollection<Team> Teams { get; set; }
    }

    public class Team
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long TeamId { get; set; }

        public bool Winner { get; set; }

        public bool FirstBlood { get; set; }

        public bool FirstTower { get; set; }

        public bool FirstInhibitor { get; set; }

        public bool FirstDragon { get; set; }

        public bool FirstRiftHerald { get; set; }

        public bool FirstBaron { get; set; }

        public int TowerKills { get; set; }

        public int InhibitorKills { get; set; }

        public int DragonKills { get; set; }

        public int RiftHeraldKills { get; set; }

        public int BaronKills { get; set; }

        public ICollection<Participant> Participants { get; set; }
    }

    public class Participant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ParticipantId { get; set; }

        public int ChampionId { get; set; }

        public string Lane { get; set; }

        public string Role { get; set; }

        public string HighestAchievedSeasonTier { get; set; }
    }
}
