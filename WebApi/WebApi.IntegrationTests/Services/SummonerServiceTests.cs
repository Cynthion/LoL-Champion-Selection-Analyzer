using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.Services;
using WebApi.Services.Interfaces;
using WebApi.Misc;

namespace WebApi.IntegrationTests.Services
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
            Console.WriteLine($"{ nameof(SummonerServiceTests) } retrieved: { string.Join(", ", pairs.Select(d => d.Value.ToString())) }");

            // assert
            Assert.AreEqual(1, pairs.Length);
            Assert.AreEqual(summonerName, pairs.First().Value.Name);
        }

        private static ISummonerService GetSummonerService()
        {
            // TODO use DI
            var apiKey = RiotApiKey.CreateFromFile();

            var regionSelector = new RegionSelector();
            var webService = new WebService(regionSelector, apiKey);

            return new SummonerService(webService);
        }
    }
}
