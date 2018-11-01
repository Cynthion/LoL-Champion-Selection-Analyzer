using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace ChampionSelectionAnalyzer.RiotApiClient.Misc
{
    // see https://developer.riotgames.com/regional-endpoints.html

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum Region
    {
        EUW,
        NA
    }

    public static class RegionExtensions
    {
        public static string GetRegionEndpoint(this Region region)
        {
            switch (region)
            {
                case Region.EUW: return "euw1.api.riotgames.com";
                case Region.NA: return "na1.api.riotgames.com";
                default:
                    return "euw1.api.riotgames.com";
            }
        }
    }
}
