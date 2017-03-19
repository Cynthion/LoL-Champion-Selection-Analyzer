using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebApi.UnitTests
{
    [TestClass]
    public class RiotApiKeyTests
    {
        [TestMethod]
        public void GivenKeyJson_WhenCreateFromFile_ThenCreationIsSuccessful()
        {
            // arrange
            // TODO add test file and assert existence

            // act
            var apiKey = RiotApiKey.CreateFromFile();

            // assert
            Assert.AreEqual(false, apiKey.IsProduction);
            Assert.IsNotNull(apiKey.ApiKey);
            Assert.AreNotEqual(string.Empty, apiKey.ApiKey);
        }
    }
}
