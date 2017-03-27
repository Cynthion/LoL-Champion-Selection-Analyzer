using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.RiotApiClient;
using WebApi.RiotApiClient.Misc;
using WebApi.RiotApiClient.Services;
using WebApi.RiotApiClient.Services.Interfaces;

namespace WebApi.Tests.IntegrationTests.RiotApiClient.Services
{
    [TestClass]
    public class LeagueServiceTests
    {
        [TestMethod]
        public void GivenQueue_WhenMasterTierLeagueIsRequested_ThenResponseIsSuccessful()
        {
            // arrange
            const string queueType = Constants.RankedSolo5V5;

            // act
            var leagueDto = GetLeagueService().GetMasterTierLeagues(Region.EUW, queueType).Result;

            // assert
            Assert.IsNotNull(leagueDto);
        }

        [TestMethod]
        public void GivenQueue_WhenChallengerTierLeagueIsRequested_ThenResponseIsSuccessful()
        {
            // arrange
            const string queueType = Constants.RankedSolo5V5;

            // act
            var leagueDto = GetLeagueService().GetChallengerTierLeagues(Region.EUW, queueType).Result;

            // assert
            Assert.IsNotNull(leagueDto);
        }

        private static ILeagueService GetLeagueService()
        {
            // TODO use DI
            var apiKey = RiotApiKey.CreateFromFile();

            var webService = new RiotWebService(apiKey);

            return new LeagueService(webService);
        }
    }
}
