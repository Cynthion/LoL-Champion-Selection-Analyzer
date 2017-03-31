using System.Collections.Generic;

namespace WebApi.Model.Dtos.Match
{
    public class MatchListDto
    {
        public List<MatchReferenceDto> Matches { get; set; }

        public int TotalGames { get; set; }

        public int StartIndex { get; set; }

        public int EndIndex { get; set; }
    }
}
