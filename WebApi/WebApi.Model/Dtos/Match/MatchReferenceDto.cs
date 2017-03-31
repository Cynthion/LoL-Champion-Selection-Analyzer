namespace WebApi.Model.Dtos.Match
{
    public class MatchReferenceDto
    {
        public long MatchId { get; set; }

        public long Champion { get; set; }

        public long Timestamp { get; set; }

        public string Season { get; set; }

        public string Region { get; set; }

        public string Queue { get; set; }

        public string Lane { get; set; }

        public string Role { get; set; }

        public string PlatformId { get; set; }
    }
}
