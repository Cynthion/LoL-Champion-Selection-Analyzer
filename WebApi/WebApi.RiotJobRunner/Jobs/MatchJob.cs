using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NLog;
using WebApi.RiotApiClient;
using WebApi.RiotApiClient.Dtos.Match;
using WebApi.RiotApiClient.Misc;
using WebApi.RiotApiClient.Services.Interfaces;
using WebApi.RiotUtils;

namespace WebApi.RiotJobRunner.Jobs
{
    internal class MatchJob : JobBase<IEnumerable<Matchup>>
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        private readonly Region _region;
        private readonly long _matchId;
        private readonly IMatchService _matchService;

        public MatchJob(
            Region region,
            long matchId,
            IMatchService matchService,
            Action<IEnumerable<Matchup>> resultAction)
            : base(resultAction)
        {
            _region = region;
            _matchId = matchId;
            _matchService = matchService;
        }

        protected override async Task<IEnumerable<Matchup>> DoWorkAsync(CancellationToken cancellationToken)
        {
            var matchDetailDto = await _matchService.GetMatchAsync(_region, _matchId);

            var matchups = CalculateMatchups(matchDetailDto);

            Logger.Debug($"{GetParameterString()}: Found {matchups.Count} matchups.");

            return matchups;
        }

        private static IList<Matchup> CalculateMatchups(MatchDetailDto matchDetailDto)
        {
            var matchups = new List<Matchup>();
            if (matchDetailDto?.Participants != null && matchDetailDto.Participants.Any())
            {
                foreach (var p1 in matchDetailDto.Participants)
                {
                    foreach (var p2 in matchDetailDto.Participants)
                    {
                        if (p2.ParticipantId == p1.ParticipantId)
                        {
                            continue;
                        }

                        var matchup = new Matchup
                        {
                            ChampionPlacement1 = new ChampionPlacement
                            {
                                ChampionId = p1.ChampionId,
                                Lane = GetLane(p1.Timeline.Lane, p2.Timeline.Role)
                            },
                            ChampionPlacement2 = new ChampionPlacement
                            {
                                ChampionId = p2.ChampionId,
                                Lane = GetLane(p2.Timeline.Lane, p2.Timeline.Role)
                            },
                            IsWin = GetIsWinner(p1.TeamId, matchDetailDto),
                            IsSameTeam = p1.TeamId == p2.TeamId
                        };

                        matchups.Add(matchup);
                    }
                }
            }

            return matchups;
        }

        private static Lane GetLane(string lane, string role)
        {
            switch (lane)
            {
                case GameConstants.TopLane: return Lane.Top;
                case GameConstants.JungleLane: return Lane.Jgl;
                case GameConstants.MiddleLane: return Lane.Mid;
                case GameConstants.BottomLane:
                    switch (role)
                    {
                        case GameConstants.NoneRole:
                        case GameConstants.SoloRole:
                        case GameConstants.DuoRole:
                        case GameConstants.CarryRole: return Lane.Bot;
                        case GameConstants.SupportRole: return Lane.Sup;
                        default:
                            throw new NotSupportedException($"{lane} / {role}");
                    }
                default:
                    throw new NotSupportedException($"{lane} / {role}");
            }
        }

        private static bool GetIsWinner(long teamId, MatchDetailDto matchDetailDto)
        {
            return matchDetailDto.Teams.Single(t => t.TeamId == teamId).Winner;
        }

        public override string ToString()
        {
            return $"{GetType().Name}{GetParameterString()}>";
        }

        private string GetParameterString()
        {
            return $"<{_region}, Match ID: {_matchId}>";
        }
    }
}
