namespace WebApi
{
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
