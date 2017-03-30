using System.Collections.Generic;

namespace WebApi.RiotApiClient
{
    public class GameConstants
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

        public const string TopLane = "TOP";
        public const string JungleLane = "JUNGLE";
        public const string MiddleLane = "MIDDLE";
        public const string BottomLane = "BOTTOM";

        public const string CarryRole = "DUO_CARRY";
        public const string SupportRole = "DUO_SUPPORT";
        public const string SoloRole = "SOLO";
        public const string DuoRole = "DUO";
        public const string NoneRole = "NONE";

        public static IList<string> GetRankedQueueTypes()
        {
            return new []
            {
                RankedSolo5V5,
                //RankedTeam5V5,
                //TeamBuilderRankedSolo,
                RankedFlexSr
            };
        }

        public static IList<string> GetCurrentSeasons()
        {
            // TODO autogenerate currently relevant seasons
            return new[]
            {
                "SEASON2016"
                //$"PRESEASON{dateTime.Year}"
                //$"SEASON{dateTime.Year}"
            };
        }
    }
}
