using System.Collections.Generic;

namespace WebApi.RiotApiClient.Dtos.Stats
{
    public class RankedStatsDto
    {
        /// <summary>
        /// Date stats were last modified specified as epoch milliseconds.
        /// </summary>
        public long ModifyDate { get; set; }

        /// <summary>
        /// Collection of aggregated stats summarized by champion.
        /// </summary>
        public IEnumerable<ChampionStatsDto> Champions { get; set; }

        public long SummonerId { get; set; }
    }
}
