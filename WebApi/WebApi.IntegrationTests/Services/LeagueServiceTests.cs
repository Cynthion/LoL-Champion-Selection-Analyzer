using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.Misc;
using WebApi.Services.RiotApi;
using WebApi.Services.RiotApi.Interfaces;

namespace WebApi.IntegrationTests.Services
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

            var regionSelector = new RegionSelector();
            var webService = new RiotWebService(regionSelector, apiKey);

            return new LeagueService(webService);
        }
    }
}
