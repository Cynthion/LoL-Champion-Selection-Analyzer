using System.Collections.Generic;
using WebApi.Core.Misc;
using WebApi.Core.Models;

namespace WebApi.Core.Services.Interfaces
{
    public interface ISuggestionService
    {
        Suggestion GetSuggestion(TeamConstellation friendly, TeamConstellation enemy, IEnumerable<long> championBans);
    }
}
