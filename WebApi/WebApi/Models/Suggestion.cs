using System.Collections.Generic;
using WebApi.Misc;

namespace WebApi.Models
{
    public class Suggestion
    {
        public IEnumerable<ChampionPotential> TopSuggestions { get; set; }

        public IEnumerable<ChampionPotential> JglSuggestions { get; set; }

        public IEnumerable<ChampionPotential> MidSuggestions { get; set; }

        public IEnumerable<ChampionPotential> BotSuggestions { get; set; }

        public IEnumerable<ChampionPotential> SupSuggestions { get; set; }
    }
}
