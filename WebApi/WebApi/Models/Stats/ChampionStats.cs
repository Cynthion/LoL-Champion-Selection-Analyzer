namespace WebApi.Models.Stats
{
    public class ChampionStats
    {
        /// <summary>
        /// Champion ID. Note that champion ID 0 represents the combined stats for all champions.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Aggregated stats associated with the champion.
        /// </summary>
        public AggregatedStats Stats { get; set; }
    }
}
