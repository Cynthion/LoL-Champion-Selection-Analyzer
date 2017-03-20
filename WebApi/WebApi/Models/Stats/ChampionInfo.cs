using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models.Stats
{
    public class ChampionInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        /// <summary>
        /// Champion ID. Note that champion ID 0 represents the combined stats for all champions.
        /// </summary>
        public int ChampionId { get; set; }

        /// <summary>
        /// Aggregated stats associated with the champion.
        /// </summary>
        public AggregatedInfo AggregatedInfo { get; set; }
    }
}
