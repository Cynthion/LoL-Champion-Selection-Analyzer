namespace WebApi.Model.Dtos.Stats
{
    public class ChampionStatsDto
    {
        /// <summary>
        /// Aggregated stats associated with the champion.
        /// </summary>
        public AggregatedStatsDto Stats  { get; set; }

        /// <summary>
        /// Champion ID. Note that champion ID 0 represents the combined stats for all champions.
        /// </summary>
        public int Id { get; set; }
    }
}
