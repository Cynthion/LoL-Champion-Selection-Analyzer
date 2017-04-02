namespace WebApi.Model.RiotDtos.League
{
    /// <summary>
    /// Unnecessary properties not contained.
    /// </summary>
    public class LeagueEntryDto
    {
        /// <summary>
        /// The league points of the participant.
        /// </summary>
        public int LeaguePoints { get; set; }

        /// <summary>
        /// The number of wins for the participant.
        /// </summary>
        public int Wins { get; set; }

        /// <summary>
        /// The number of losses for the participant.
        /// </summary>
        public int Losses { get; set; }
        
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

        public string Playstyle { get; set; }

        public bool IsFreshBlood { get; set; }

        public bool IsHotStreak { get; set; }

        public bool IsInactive { get; set; }

        public bool IsVeteran { get; set; }
    }
}
