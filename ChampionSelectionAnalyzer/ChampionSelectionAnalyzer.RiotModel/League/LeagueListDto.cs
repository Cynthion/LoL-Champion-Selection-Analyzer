using System.Collections.Generic;

namespace ChampionSelectionAnalyzer.RiotModel.League
{
    public class LeagueListDto
    {
        public string LeagueId { get; set; }
        public string Tier { get; set; }
        public List<LeagueItemDto> Entries { get; set; }
        public string Queue { get; set; }
        public string Name { get; set; }

        // non-Riot property
        public string Region { get; set; }
    }
}
