using System.Collections.Generic;

namespace WebApi.Model.Dtos.League
{
    /// <summary>
    /// Unnecessary properties not contained.
    /// </summary>
    public class League
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public long Key { get; set; }

        /// <summary>
        /// Specifies the relevant participant that is a member of this league 
        /// (i.e., a requested summoner ID, a requested team ID, or the ID of 
        /// a team to which one of the requested summoners belongs). Only 
        /// present when full league is requested so that participant's entry 
        /// can be identified. Not present when individual entry is requested.
        /// </summary>
        public long ParticipantId { get; set; }

        /// <summary>
        /// The league's queue type.
        /// </summary>
        public string Queue { get; set; } // TODO make enum

        /// <summary>
        /// The league's tier.
        /// </summary>
        public string Tier { get; set; } // TODO make enum

        public string Name { get; set; }

        /// <summary>
        /// The requested league entries.
        /// </summary>
        public ICollection<LeagueEntry> Entries { get; set; }
    }
}
