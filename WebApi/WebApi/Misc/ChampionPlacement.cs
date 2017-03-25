namespace WebApi.Misc
{
    public struct ChampionPlacement
    {
        public long ChampionId { get; set; }

        public Lane Lane { get; set; }

        /// <summary>
        /// Indicates whether a champion is announced or picked.
        /// </summary>
        public bool IsPicked => ChampionId != 0;
    }
}
