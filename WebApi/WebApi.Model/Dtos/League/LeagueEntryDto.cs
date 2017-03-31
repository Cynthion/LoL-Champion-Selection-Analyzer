using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Model.Dtos.League
{
    /// <summary>
    /// Unnecessary properties not contained.
    /// </summary>
    public class LeagueEntryDto
    {
        /// <summary>
        /// The ID of the participant (i.e., summoner or team) represented by this entry.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string PlayerOrTeamId { get; set; }

        /// <summary>
        /// The name of the the participant (i.e., summoner or team) represented by this entry.
        /// </summary>
        public string PlayerOrTeamName { get; set; }

        /// <summary>
        /// The league division of the participant.
        /// </summary>
        public string Division { get; set; }
    }
}
