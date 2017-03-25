namespace WebApi.Misc
{
    public struct ChampionPotential
    {
        public long ChampionId { get; private set; }

        public double AvgSynergyWinRate { get; set; }

        public double AvgCounterWinRate { get; set; }

        public double Potential => AvgSynergyWinRate - AvgCounterWinRate;

        public ChampionPotential(long championId)
        {
            ChampionId = championId;
            AvgSynergyWinRate = 0;
            AvgCounterWinRate = 0;
        }
    }
}
