namespace WebApi.Models.Dtos.League
{
    /// <summary>
    /// Unnecessary properties not contained.
    /// </summary>
    public class LeagueEntryDto
    {
        /// <summary>
        /// The league division of the participant.
        /// </summary>
        public string Division { get; set; }

        /// <summary>
        /// The ID of the participant (i.e., summoner or team) represented by this entry.
        /// </summary>
        public string PlayerOrTeamId { get; set; }

        /// <summary>
        /// The name of the the participant (i.e., summoner or team) represented by this entry.
        /// </summary>
        public string PlayerOrTeamName { get; set; }
    }
}
