using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.Services;
using WebApi.Services.Interfaces;

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
            var pairs = GetSummonerService().GetSummonersByNameAsync(new[] {summonerName}).Result.ToArray();
            Console.WriteLine($"{ nameof(SummonerServiceTests) } retrieved: { string.Join(", ", pairs.Select(d => d.Value.ToString())) }");

            // assert
            Assert.AreEqual(1, pairs.Length);
            Assert.AreEqual(summonerName, pairs.First().Value.Name);
        }

        private static ISummonerService GetSummonerService()
        {
            // TODO use DI
            var apiKey = RiotApiKey.CreateFromFile();

            var webService = new WebService(apiKey);
            var regionSelector = new RegionSelector();

            return new SummonerService(webService, regionSelector);
        }
    }
}
