using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Model.Entities
{
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
