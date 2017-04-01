using System.Collections.Generic;

namespace WebApi.Model.Dtos.Match
{
    public class MatchList
    {
        public int StartIndex { get; set; }

        public int EndIndex { get; set; }

        public int TotalGames { get; set; }

        public List<MatchReference> Matches { get; set; }
    }
}
