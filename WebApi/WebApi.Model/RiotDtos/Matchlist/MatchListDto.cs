using System.Collections.Generic;

namespace WebApi.Model.RiotDtos.Matchlist
{
    public class MatchListDto
    {
        public int TotalGames { get; set; }

        public int StartIndex { get; set; }

        public int EndIndex { get; set; }

        public List<MatchReferenceDto> Matches { get; set; }
    }
}
