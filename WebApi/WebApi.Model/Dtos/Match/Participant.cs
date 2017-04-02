namespace WebApi.Model.Dtos.Match
{
    /// <summary>
    /// Unnecessary properties not contained.
    /// </summary>
    public class Participant
    {
        public int ParticipantId { get; set; }

        public int TeamId { get; set; }

        public int ChampionId { get; set; }

        //public ParticipantStats Stats { get; set; }

        #region Timeline

        //public ParticipantTimeline Timeline { get; set; }

        public string Lane { get; set; }

        public string Role { get; set; }

        #endregion
    }
}
