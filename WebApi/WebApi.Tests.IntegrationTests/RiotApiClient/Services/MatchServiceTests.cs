using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.RiotApiClient;
using WebApi.RiotApiClient.Misc;
using WebApi.RiotApiClient.Services;
using WebApi.RiotApiClient.Services.Interfaces;

namespace WebApi.Tests.IntegrationTests.RiotApiClient.Services
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
                GameConstants.RankedSolo5V5,
                GameConstants.RankedTeam5V5,
                GameConstants.TeamBuilderRankedSolo,
                GameConstants.RankedFlexSr
            };

            var seasons = GameConstants.GetCurrentSeasons();

            // act
            var matchListDto = GetMatchService().GetMatchListAsync(Region.EUW, championId, rankedQueues, seasons).Result;

            // assert
            Assert.IsNotNull(matchListDto);
            //Assert.IsTrue(matchListDto.StartIndex > 0);
            //Assert.IsTrue(matchListDto.EndIndex > 0);
            Assert.IsNotNull(matchListDto.Matches);
            Assert.IsTrue(matchListDto.Matches.Count > 0);
            Assert.IsNotNull(matchListDto.Matches.First().MatchId);
            Assert.IsNotNull(matchListDto.Matches.First().Champion);
            Assert.IsNotNull(matchListDto.Matches.First().Timestamp);
            Assert.IsNotNull(matchListDto.Matches.First().Season);
            Assert.IsFalse(string.IsNullOrEmpty(matchListDto.Matches.First().Region));
            Assert.IsFalse(string.IsNullOrEmpty(matchListDto.Matches.First().Queue));
            Assert.IsFalse(string.IsNullOrEmpty(matchListDto.Matches.First().Lane));
            Assert.IsFalse(string.IsNullOrEmpty(matchListDto.Matches.First().Role));
            Assert.IsFalse(string.IsNullOrEmpty(matchListDto.Matches.First().PlatformId));
        }

        [TestMethod]
        public void GivenMatchId_WhenMatchIsRequested_ThenResponseIsSuccessful()
        {
            // arrange
            const long matchId = 2852190087;

            // act
            var matchDetailDto = GetMatchService().GetMatchAsync(Region.EUW, matchId).Result;

            // assert
            Assert.IsNotNull(matchDetailDto);
            Assert.IsNotNull(matchDetailDto.MatchId);
            Assert.IsFalse(string.IsNullOrEmpty(matchDetailDto.Season));
            Assert.IsFalse(string.IsNullOrEmpty(matchDetailDto.Region));
            Assert.IsFalse(string.IsNullOrEmpty(matchDetailDto.QueueType));
            Assert.AreEqual(matchDetailDto.Teams.Count, 2);
            Assert.IsTrue(matchDetailDto.Teams[0].TeamId > 0);
            Assert.IsTrue(matchDetailDto.Teams[1].TeamId > 0);
            Assert.AreEqual(1, matchDetailDto.Teams.Count(t => t.Winner));
            Assert.AreEqual(1, matchDetailDto.Teams.Count(t => !t.Winner));
        }

        private static IMatchService GetMatchService()
        {
            // TODO use DI
            var webService = RiotWebService.Instance;

            return new MatchService(webService);
        }
    }
}
