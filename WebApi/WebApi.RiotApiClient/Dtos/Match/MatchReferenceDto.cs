namespace WebApi.RiotApiClient.Dtos.Match
{
    public class MatchReferenceDto
    {
        public string Lane { get; set; }

        public long Champion { get; set; }

        public string PlatformId { get; set; }

        public long Timestamp { get; set; }

        public string Region { get; set; }

        public long MatchId { get; set; }

        public string Queue { get; set; }

        public string Role { get; set; }

        public string Season { get; set; }
    }
}
