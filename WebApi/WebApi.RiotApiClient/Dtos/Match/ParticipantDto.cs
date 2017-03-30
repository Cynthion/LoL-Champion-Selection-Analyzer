namespace WebApi.RiotApiClient.Dtos.Match
{
    /// <summary>
    /// Unnecessary properties not contained.
    /// </summary>
    public class ParticipantDto
    {
        public int ParticipantId { get; set; }

        public int ChampionId { get; set; }

        public int TeamId { get; set; }

        public ParticipantTimelineDto Timeline { get; set; }
    }
}
