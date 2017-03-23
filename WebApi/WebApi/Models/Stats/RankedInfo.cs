using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models.Stats
{
    public class RankedInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long SummonerId { get; set; }

        /// <summary>
        /// Date stats were last modified specified as epoch milliseconds.
        /// </summary>
        public long ModifyDate { get; set; }

        /// <summary>
        /// Collection of aggregated stats summarized by champion.
        /// </summary>
        public ICollection<ChampionInfo> ChampionInfos { get; set; }
    }
}
