namespace ChampionSelectionAnalyzer.RiotApiClient.Misc
{
    internal static class Util
    {
        internal static string AddUrlParameter(this string url, string parameter)
        {
            return $"{url}{(url.Contains("?") ? "&" : "?")}{parameter}";
        }
    }
}
