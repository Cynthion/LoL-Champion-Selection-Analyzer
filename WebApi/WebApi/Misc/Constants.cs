using System;

namespace WebApi.Misc
{
    public class Constants
    {
        /// <summary>
        /// Ranked Solo 5v5 games.
        /// </summary>
        public const string RankedSolo5V5 = "RANKED_SOLO_5x5";

        /// <summary>
        /// Ranked Team 5v5 games.
        /// </summary>
        public const string RankedTeam5V5 = "RANKED_TEAM_5x5";

        /// <summary>
        /// Ranked Solo games from current season that use Team Builder matchmaking.
        /// </summary>
        public const string TeamBuilderRankedSolo = "TEAM_BUILDER_RANKED_SOLO";

        /// <summary>
        /// Ranked Flex Summoner's Rift games.
        /// </summary>
        public const string RankedFlexSr = "RANKED_FLEX_SR";

        public static string GetSeasonForYear(DateTime dateTime)
        {
            return $"SEASON{dateTime.Year}";
        }
    }
}
