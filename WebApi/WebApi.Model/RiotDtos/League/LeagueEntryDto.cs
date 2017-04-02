namespace WebApi.Model.RiotDtos.League
{
    /// <summary>
    /// Unnecessary properties not contained.
    /// </summary>
    public class LeagueEntryDto
    {
        /// <summary>
        /// The ID of the participant (i.e., summoner or team) represented by this entry.
        /// </summary>
        public long PlayerOrTeamId { get; set; }

        /// <summary>
        /// The name of the the participant (i.e., summoner or team) represented by this entry.
        /// </summary>
        public string PlayerOrTeamName { get; set; }

        /// <summary>
        /// The league division of the participant.
        /// </summary>
        public string Division { get; set; }

        public string Region { get; set; } // TODO make enum
    }
}
