using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Model.Entities
{
    public class SummonerMatch
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Key { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SummonerId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long MatchId { get; set; }

        public long Timestamp { get; set; }

        public long Champion { get; set; }

        public string Season { get; set; }

        public string Region { get; set; }

        public string Queue { get; set; }

        public string Lane { get; set; }

        public string Role { get; set; }
    }
}
