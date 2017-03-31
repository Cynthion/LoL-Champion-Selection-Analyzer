using Microsoft.AspNetCore.Mvc;
using WebApi.Core.Models;
using WebApi.Core.Services.Interfaces;

namespace WebApi.Core.Controllers
{
    [Route("api/[championanalyzer]")]
    public class SuggestionController : Controller
    {
        private readonly ISuggestionService _suggestionService;

        public SuggestionController(ISuggestionService suggestionService)
        {
            _suggestionService = suggestionService;
        }

        [HttpGet]
        public IActionResult GetSuggestions([FromBody] SuggestionRequestDto request)
        {
            var suggestion = _suggestionService.GetSuggestion(request.Team1, request.Team2, request.ChampionBans);

            var response = new SuggestionResponseDto
            {
                TopSuggestions = suggestion.TopSuggestions,
                JglSuggestions = suggestion.JglSuggestions,
                MidSuggestions = suggestion.MidSuggestions,
                BotSuggestions = suggestion.BotSuggestions,
                SupSuggestions = suggestion.SupSuggestions
            };

            return new ObjectResult(response);
        }
    }
}
