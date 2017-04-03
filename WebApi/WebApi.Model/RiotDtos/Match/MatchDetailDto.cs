using System.Collections.Generic;

namespace WebApi.Model.RiotDtos.Match
{
    /// <summary>
    /// Unnecessary properties not contained.
    /// </summary>
    public class MatchDetailDto
    {
        public long MatchId { get; set; }

        public long MatchCreation { get; set; }

        public long MatchDuration { get; set; }

        public int MapId { get; set; }

        public string Season { get; set; }

        public string Region { get; set; }

        public string QueueType { get; set; }

        public string MatchVersion { get; set; }

        public string PlatformId { get; set; }

        public IEnumerable<TeamDto> Teams { get; set; }

        public IEnumerable<ParticipantDto> Participants { get; set; }

        public IEnumerable<ParticipantIdentityDto> ParticipantIdentities { get; set; }
    }
}
