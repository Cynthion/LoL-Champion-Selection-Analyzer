using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Model.Dtos.Match
{
    /// <summary>
    /// Unnecessary properties not contained.
    /// </summary>
    public class MatchDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long MatchId { get; set; }

        public string Season { get; set; }

        public string Region { get; set; }

        public string QueueType { get; set; }

        public long MatchCreation { get; set; }

        public long MatchDuration { get; set; }

        public ICollection<Team> Teams { get; set; }

        public ICollection<Participant> Participants { get; set; }
    }
}
