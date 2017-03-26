namespace WebApi.Services
{
    internal static class Util
    {
        internal static void AddUrlParameter(this string url, string parameter)
        {
            url = $"{url}{(url.Contains("?") ? "&" : "?")}{parameter}";
        }
    }
}
