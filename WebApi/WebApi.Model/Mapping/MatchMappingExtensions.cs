using System.Collections.Generic;
using System.Linq;
using WebApi.Model.Entities;
using WebApi.Model.RiotDtos.Match;
using WebApi.Model.RiotDtos.Matchlist;

namespace WebApi.Model.Mapping
{
    public static class MatchMappingExtensions
    {
        public static SummonerMatch ToEntity(this MatchReferenceDto dto, long playerId)
        {
            return new SummonerMatch
            {
                MatchId = dto.MatchId,
                SummonerId = playerId,
                Timestamp = dto.Timestamp,
                Champion = dto.Champion,
                Season = dto.Season,
                Region = dto.Region,
                Queue = dto.Queue,
                Lane = dto.Lane,
                Role = dto.Role
            };
        }

        public static Match ToEntity(this MatchDetailDto dto)
        {
            return new Match
            {
                MatchId = dto.MatchId,
                MatchCreation = dto.MatchCreation,
                MatchDuration = dto.MatchDuration,
                Season = dto.Season,
                Region = dto.Region,
                QueueType = dto.QueueType,
                Teams = dto.Teams.Select(t => t.ToEntity(dto.Participants)).ToArray()
            };
        }

        public static Team ToEntity(this TeamDto dto, IEnumerable<ParticipantDto> participantDtos)
        {
            return new Team
            {
                Winner = dto.Winner,
                FirstBlood = dto.FirstBlood,
                FirstTower = dto.FirstTower,
                FirstInhibitor = dto.FirstInhibitor,
                FirstDragon = dto.FirstDragon,
                FirstRiftHerald = dto.FirstRiftHerald,
                FirstBaron = dto.FirstBaron,
                TowerKills = dto.TowerKills,
                InhibitorKills = dto.InhibitorKills,
                DragonKills = dto.DragonKills,
                RiftHeraldKills = dto.RiftHeraldKills,
                BaronKills = dto.BaronKills,
                Participants = participantDtos.Select(p => p.ToEntity()).ToArray()
            };
        }

        public static Participant ToEntity(this ParticipantDto dto)
        {
            return new Participant
            {
                ChampionId = dto.ChampionId,
                Lane = dto.Timeline.Lane,
                Role = dto.Timeline.Role,
                HighestAchievedSeasonTier = dto.HighestAchievedSeasonTier
            };
        }
    }
}
