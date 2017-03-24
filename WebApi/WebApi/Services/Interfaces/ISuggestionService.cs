using WebApi.Misc;
using WebApi.Models;

namespace WebApi.Services.Interfaces
{
    public interface ISuggestionService
    {
        Suggestion GetSuggestion(TeamConstellation own, TeamConstellation enemy);
    }
}
