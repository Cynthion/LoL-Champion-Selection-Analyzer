using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Model.Enums;

namespace WebApi.Model.Model.League
{
    public class SummonerLeagueEntry
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long PlayerId { get; set; }

        public int LeaguePoints { get; set; }

        public int Wins { get; set; }

        public int Losses { get; set; }

        public Region Region { get; set; }
    }
}
