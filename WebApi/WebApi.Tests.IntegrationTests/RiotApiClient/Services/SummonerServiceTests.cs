using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.RiotApiClient.Misc;
using WebApi.RiotApiClient.Services;
using WebApi.RiotApiClient.Services.Interfaces;

namespace WebApi.Tests.IntegrationTests.RiotApiClient.Services
{
    [TestClass]
    public class SummonerServiceTests
    {
        [TestMethod]
        public void GivenSummonerName_WhenGetSummonersByName_ThenResponseIsSuccessful()
        {
            // arrange
            const string summonerName = "Drohkan";

            // act
            var pairs = GetSummonerService().GetSummonersByNameAsync(Region.EUW, new[] {summonerName}).Result.ToArray();

            // assert
            Assert.AreEqual(1, pairs.Length);
            Assert.AreEqual(summonerName, pairs.First().Value.Name);
        }

        private static ISummonerService GetSummonerService()
        {
            // TODO use DI
            var apiKey = RiotApiKey.CreateFromFile();

            var webService = new RiotWebService(apiKey);

            return new SummonerService(webService);
        }
    }
}
