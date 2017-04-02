namespace WebApi.Model.RiotDtos.Match
{
    public class PlayerDto
    {
        public long SummonerId { get; set; }

        public string SummonerName { get; set; }

        public int ProfileIcon { get; set; }

        public string MatchHistoryUri { get; set; }
    }
}
