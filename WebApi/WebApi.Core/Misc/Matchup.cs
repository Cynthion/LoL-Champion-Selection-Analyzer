namespace WebApi.Core.Misc
{
    /// <summary>
    /// Represents a matchup where <see cref="ChampionPlacement1"/> plays versus <see cref="ChampionPlacement2"/>.
    /// </summary>
    public class Matchup
    {
        public ChampionPlacement ChampionPlacement1 { get; set; }

        public ChampionPlacement ChampionPlacement2 { get; set; }

        public bool IsSameTeam { get; set; }

        public bool IsWin { get; set; }
    }
}
