namespace ChampionSelectionAnalyzer.RiotModel.Match
{
    public class MatchReferenceDto
    {
        public string Lane { get; set; }
        public object GameId { get; set; }
        public int Champion { get; set; }
        public string PlatformId { get; set; }
        public int Season { get; set; }
        public int Queue { get; set; }
        public string Role { get; set; }
        public long Timestamp { get; set; }
    }
}