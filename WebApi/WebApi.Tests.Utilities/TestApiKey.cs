using WebApi.Misc.Interfaces;

namespace WebApi.Tests.Utilities
{
    public class TestApiKey : IApiKey
    {
        public bool IsProduction => false;

        public string ApiKey => "TestApiKey";
    }
}
