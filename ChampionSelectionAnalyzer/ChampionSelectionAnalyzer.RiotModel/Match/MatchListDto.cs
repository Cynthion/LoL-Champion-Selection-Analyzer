using System.Collections.Generic;

namespace ChampionSelectionAnalyzer.RiotModel.Match
{
    public class MatchListDto
    {
        public List<MatchReferenceDto> Matches { get; set; }
        public int TotalGames { get; set; }
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }
    }

    public class MatchReferenceDto
    {
        public string Lane { get; set; }
        public object GameId { get; set; }
        public int Champion { get; set; }
        public string PlatformId { get; set; }
        public int Season { get; set; }
        public int Queue { get; set; }
        public string Role { get; set; }
        public long Timestamp { get; set; }
    }
}
