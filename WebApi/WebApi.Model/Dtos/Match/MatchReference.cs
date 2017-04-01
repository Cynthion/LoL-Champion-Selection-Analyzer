using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Model.Dtos.Match
{
    public class MatchReference
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long MatchId { get; set; }

        public long Champion { get; set; }

        public long Timestamp { get; set; }

        public string Season { get; set; }

        public string Region { get; set; }

        public string Queue { get; set; }

        public string Lane { get; set; }

        public string Role { get; set; }

        public string PlatformId { get; set; }

        public override string ToString()
        {
            return $"{nameof(MatchReference)}<{Region},{MatchId}>";
        }
    }
}
