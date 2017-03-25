namespace WebApi.Misc
{
    /// <summary>
    /// Represents a matchup where <see cref="ChampionPlacement1"/> plays versus <see cref="ChampionPlacement2"/>.
    /// </summary>
    public class Matchup
    {
        public ChampionPlacement ChampionPlacement1 { get; private set; }

        public ChampionPlacement ChampionPlacement2 { get; private set; }

        /// <summary>
        /// The win rate for <see cref="ChampionPlacement1"/> when he plays with <see cref="ChampionPlacement2"/>.
        /// </summary>
        public long SynergyWinRate { get; private set; }

        /// <summary>
        /// The win rate for <see cref="ChampionPlacement1"/> when he plays against <see cref="ChampionPlacement2"/>.
        /// </summary>
        public long CounterWinRate { get; private set; }

        public Matchup(ChampionPlacement championPlacement1, ChampionPlacement championPlacement2)
        {
            ChampionPlacement1 = championPlacement1;
            ChampionPlacement2 = championPlacement2;

            SynergyWinRate = 0;
            CounterWinRate = 0;
        }

        public void Update()
        {
            //CounterWinRate = ...
            //SynergyWinRate = ...
        }
    }
}
