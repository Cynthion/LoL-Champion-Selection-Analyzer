using WebApi.Model.Entities;
using WebApi.Model.Enums;
using WebApi.Model.RiotDtos.League;

namespace WebApi.Model.Mapping
{
    public static class LeagueMappingExtensions
    {
        public static SummonerLeagueEntry ToEntity(this LeagueEntryDto dto, Region region)
        {
            return new SummonerLeagueEntry
            {
                PlayerId = dto.PlayerOrTeamId,
                LeaguePoints = dto.LeaguePoints,
                Wins = dto.Wins,
                Losses = dto.Losses,
                Region = region
            };
        }
    }
}
