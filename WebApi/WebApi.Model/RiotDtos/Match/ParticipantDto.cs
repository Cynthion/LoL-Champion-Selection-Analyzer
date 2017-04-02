using System.Collections.Generic;

namespace WebApi.Model.RiotDtos.Match
{
    /// <summary>
    /// Unnecessary properties not contained.
    /// </summary>
    public class ParticipantDto
    {
        public int ParticipantId { get; set; }

        public int TeamId { get; set; }

        public int ChampionId { get; set; }

        public ParticipantStatsDto Stats { get; set; }

        public ICollection<RuneDto> Runes { get; set; }

        public ICollection<MasteryDto> Masteries { get; set; }

        public ParticipantTimelineDto Timeline { get; set; }

        public string HighestAchievedSeasonTier { get; set; }
    }
}
