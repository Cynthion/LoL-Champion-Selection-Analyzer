using WebApi.Core.Misc;

namespace WebApi.Core.Models
{
    public class SuggestionRequestDto
    {
        public IEnumerable<long> ChampionBans { get; set; }

        public TeamConstellation Team1 { get; set; }

        public TeamConstellation Team2 { get; set; }
    }
}
