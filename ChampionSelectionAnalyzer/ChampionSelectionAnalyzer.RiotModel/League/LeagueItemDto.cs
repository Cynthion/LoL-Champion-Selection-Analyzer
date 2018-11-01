namespace ChampionSelectionAnalyzer.RiotModel.League
{
    public class LeagueItemDto
    {
        public string Rank { get; set; }
        public bool HotStreak { get; set; }
        public int Wins { get; set; }
        public bool Veteran { get; set; }
        public int Losses { get; set; }
        public bool FreshBlood { get; set; }
        public string PlayerOrTeamName { get; set; }
        public bool Inactive { get; set; }
        public string PlayerOrTeamId { get; set; }
        public int LeaguePoints { get; set; }
    }
}