using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.Misc;
using WebApi.Services.RiotApi;
using WebApi.Services.RiotApi.Interfaces;

namespace WebApi.Tests.IntegrationTests.Services
{
    [TestClass]
    public class RiotWebServiceTests
    {
        [Ignore]
        [TestMethod]
        public void GivenRiotWebService_WhenExceedingRateLimit_ThenRetryAfterIsRead()
        {
            // arrange
            var service = GetRiotWebService();
            const string url = "/api/lol/EUW/v2.2/matchlist/by-summoner/40691874";

            // act
            for (var i = 0; i < 11; i++)
            {
                service.GetRequestAsync(Region.EUW, url);
            }

            // assert
            // manually check console output for header information
        }

        private static IWebService GetRiotWebService()
        {
            return new RiotWebService(RiotApiKey.CreateFromFile());
        }
    }
}
