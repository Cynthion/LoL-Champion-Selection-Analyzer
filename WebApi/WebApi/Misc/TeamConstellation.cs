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

        /// <summary>
        /// Returns the lanes that have already been announced or picked.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ChampionPlacement> GetPickedPlacements()
        {
            // TODO use yield return, use distinct
            var picks = new List<ChampionPlacement>();

            if (Top.IsPicked) picks.Add(Top);
            if (Jgl.IsPicked) picks.Add(Jgl);
            if (Mid.IsPicked) picks.Add(Mid);
            if (Bot.IsPicked) picks.Add(Bot);
            if (Sup.IsPicked) picks.Add(Sup);

            return picks;
        }
    }
}
