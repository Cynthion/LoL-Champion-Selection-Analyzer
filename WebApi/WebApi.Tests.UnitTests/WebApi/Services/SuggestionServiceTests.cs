using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.Core.Misc;
using WebApi.Core.Services;

namespace WebApi.Tests.UnitTests.WebApi.Services
{
    [TestClass]
    public class SuggestionServiceTests
    {
        public void GivenTeamConstellations_WhenSuggestionRequested_ThenSuggestionResult()
        {
            //// arrange
            //var teamConstellation = new TeamConstellation
            //{
            //    Top = new ChampionPlacement(),
            //    Jgl = new ChampionPlacement(),
            //    Mid = new ChampionPlacement(),
            //    Bot = new ChampionPlacement(),
            //    Sup = new ChampionPlacement()
            //};

            //var service = new SuggestionService();

            //// act
            //var suggestion = service.GetSuggestion(teamConstellation, teamConstellation, Enumerable.Empty<long>());

            //// assert
            //CollectionAssert.AllItemsAreNotNull(suggestion.TopSuggestions.ToArray());
            //CollectionAssert.AllItemsAreNotNull(suggestion.JglSuggestions.ToArray());
            //CollectionAssert.AllItemsAreNotNull(suggestion.MidSuggestions.ToArray());
            //CollectionAssert.AllItemsAreNotNull(suggestion.BotSuggestions.ToArray());
            //CollectionAssert.AllItemsAreNotNull(suggestion.SupSuggestions.ToArray());
        }

        public void GivenEmptyTeamConstellations_WhenSuggestionRequested_ThenSuggestionResult()
        {
            // arrange

            // act

            // assert
        }

        public void GivenTeamConstellationsAndBans_WhenSuggestionRequested_ThenSuggestionContainsNoBannedPickedChamps()
        {
            // arrange

            // act

            // assert
        }
    }
}
