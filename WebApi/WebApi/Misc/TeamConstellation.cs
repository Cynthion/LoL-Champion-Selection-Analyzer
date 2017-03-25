using System.Collections.Generic;

namespace WebApi.Misc
{
    // TODO use enumerable instead of 5 distinct properties -> support non-meta games
    public class TeamConstellation
    {
        public ChampionPlacement Top { get; set; }

        public ChampionPlacement Jgl { get; set; }

        public ChampionPlacement Mid { get; set; }

        public ChampionPlacement Bot { get; set; }

        public ChampionPlacement Sup { get; set; }

        public IEnumerable<ChampionPlacement> ChampionPlacements => new[]
        {
            Top,
            Jgl,
            Mid,
            Bot,
            Sup
        };
    }
}
