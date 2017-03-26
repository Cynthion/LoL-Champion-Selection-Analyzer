using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.Misc;
using WebApi.Services.RiotApi;
using WebApi.Services.RiotApi.Interfaces;

namespace WebApi.IntegrationTests.Services
{
    [TestClass]
    public class MatchServiceTests
    {
        [TestMethod]
        public void GivenAllParameters_WhenMatchListIsRequested_ThenResponseIsSuccessful()
        {
            // arrange
            const long championId = 40691874; // Drohkan
            var rankedQueues = new[]
            {
                Constants.RankedSolo5V5,
                Constants.RankedTeam5V5,
                Constants.TeamBuilderRankedSolo,
                Constants.RankedFlexSr
            };

            var seasons = new[]
            {
                Constants.GetSeasonForYear(DateTime.Now),
                Constants.GetSeasonForYear(DateTime.Now.AddYears(-1)),
                Constants.GetSeasonForYear(DateTime.Now.AddYears(-2))
            };

            // act
            var matchList = GetMatchService().GetMatchListAsync(Region.EUW, championId, rankedQueues, seasons).Result;

            // assert
            Assert.IsNotNull(matchList);
        }

        private static IMatchService GetMatchService()
        {
            // TODO use DI
            var apiKey = RiotApiKey.CreateFromFile();

            var regionSelector = new RegionSelector();
            var webService = new RiotWebService(regionSelector, apiKey);

            return new MatchService(webService);
        }
    }
}
