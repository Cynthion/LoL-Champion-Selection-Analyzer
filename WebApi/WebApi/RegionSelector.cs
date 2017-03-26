namespace WebApi
{
    // Information about Regional Endpoints: https://developer.riotgames.com/regional-endpoints.html

    public class RegionSelector : IRegionSelector
    {
        private const string EuropeWest = "EUW";

        public string GetRegion()
        {
            // TODO make configurable
            return EuropeWest;
        }
    }
}
