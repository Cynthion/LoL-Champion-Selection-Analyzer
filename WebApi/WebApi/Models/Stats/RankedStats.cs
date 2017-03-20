using System.Collections.Generic;

namespace WebApi.Models.Stats
{
    public class RankedStats
    {
        public long SummonerId { get; set; }

        /// <summary>
        /// Date stats were last modified specified as epoch milliseconds.
        /// </summary>
        public long ModifyDate { get; set; }

        /// <summary>
        /// Collection of aggregated stats summarized by champion.
        /// </summary>
        public ICollection<ChampionStats> ChampionStats { get; set; }
    }
}
