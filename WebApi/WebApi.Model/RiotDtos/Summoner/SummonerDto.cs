namespace WebApi.Model.RiotDtos.Summoner
{
    public class SummonerDto
    {
        /// <summary>
        /// ID of the summoner icon associated with the summoner.
        /// </summary>
        public int ProfileIconId { get; set; }

        /// <summary>
        /// Date summoner was last modified specified as epoch milliseconds. 
        /// The following events will update this timestamp: profile icon change, 
        /// playing the tutorial or advanced tutorial, finishing a game, summoner name change.
        /// </summary>
        public long RevisionDate { get; set; }

        /// <summary>
        /// Summoner ID.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Summoner name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Summoner level associated with the summoner.
        /// </summary>
        public long SummonerLevel { get; set; }

        public override string ToString()
        {
            return $"{nameof(ProfileIconId)}: {ProfileIconId}, " +
                   $"{nameof(RevisionDate)}: {RevisionDate}, " +
                   $"{nameof(Id)}: {Id}, " +
                   $"{nameof(Name)}: {Name}, " +
                   $"{nameof(SummonerLevel)}: {SummonerLevel}";
        }
    }
}
