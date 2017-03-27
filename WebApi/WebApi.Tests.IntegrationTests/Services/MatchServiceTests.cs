using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.Misc;
using WebApi.Services.RiotApi;
using WebApi.Services.RiotApi.Interfaces;

namespace WebApi.Tests.IntegrationTests.Services
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

        [TestMethod]
        public void GivenMatchId_WhenMatchIsRequested_ThenResponseIsSuccessful()
        {
            // arrange
            const long matchId = 2852190087;

            // act
            var match = GetMatchService().GetMatchAsync(Region.EUW, matchId);

            // assert
            Assert.IsNotNull(match);
        }

        private static IMatchService GetMatchService()
        {
            // TODO use DI
            var apiKey = RiotApiKey.CreateFromFile();

            var webService = new RiotWebService(apiKey);

            return new MatchService(webService);
        }
    }
}
