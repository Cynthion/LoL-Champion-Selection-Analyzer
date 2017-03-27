using WebApi.RiotApiClient.Misc.Interfaces;

namespace WebApi.Tests.Utilities
{
    public class TestApiKey : IApiKey
    {
        public bool IsProduction => false;

        public string ApiKey => "TestApiKey";
    }
}
