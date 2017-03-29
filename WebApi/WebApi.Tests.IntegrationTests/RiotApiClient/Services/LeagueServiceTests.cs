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
            const string queueType = GameConstants.RankedSolo5V5;

            // act
            var leagueDto = GetLeagueService().GetMasterTierLeaguesAsync(Region.EUW, queueType).Result;

            // assert
            Assert.IsNotNull(leagueDto);
        }

        [TestMethod]
        public void GivenQueue_WhenChallengerTierLeagueIsRequested_ThenResponseIsSuccessful()
        {
            // arrange
            const string queueType = GameConstants.RankedSolo5V5;

            // act
            var leagueDto = GetLeagueService().GetChallengerTierLeaguesAsync(Region.EUW, queueType).Result;

            // assert
            Assert.IsNotNull(leagueDto);
        }

        private static ILeagueService GetLeagueService()
        {
            // TODO use DI
            var webService = RiotWebService.Instance;

            return new LeagueService(webService);
        }
    }
}
