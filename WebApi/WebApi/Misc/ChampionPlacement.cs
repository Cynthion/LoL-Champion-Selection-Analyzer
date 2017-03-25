namespace WebApi.Misc
{
    public struct ChampionPlacement
    {
        public long ChampionId { get; set; }

        public Lane Lane { get; set; }

        public bool IsPicked => ChampionId != 0;
    }
}
