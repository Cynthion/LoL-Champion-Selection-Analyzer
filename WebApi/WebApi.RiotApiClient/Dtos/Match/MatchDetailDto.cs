using System.Collections.Generic;

namespace WebApi.RiotApiClient.Dtos.Match
{
    /// <summary>
    /// Unnecessary properties not contained.
    /// </summary>
    public class MatchDetailDto
    {
        public long MatchId { get; set; }

        public string Region { get; set; }

        public IList<TeamDto> Teams { get; set; }

        public string Season { get; set; }

        public string QueueType { get; set; }
    }
}
