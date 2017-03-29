using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NLog;
using WebApi.RiotApiClient.Dtos.League;
using WebApi.RiotApiClient.Misc;
using WebApi.RiotApiClient.Services.Interfaces;

namespace WebApi.RiotJobRunner.Jobs
{
    internal enum TierLeague
    {
        Challenger,
        Master
    }

    internal class TierLeagueJob : JobBase<IEnumerable<long>>
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        private readonly Region _region;
        private readonly TierLeague _tierLeague;
        private readonly string _queueType;
        private readonly ILeagueService _leagueService;
        
        public TierLeagueJob(
            Region region, 
            TierLeague tierLeague,
            string queueType, 
            ILeagueService leagueService,
            Action<IEnumerable<long>> resultAction)
            : base(resultAction)
        {
            _region = region;
            _tierLeague = tierLeague;
            _queueType = queueType;
            _leagueService = leagueService;
        }

        protected override async Task<IEnumerable<long>> DoWorkAsync(CancellationToken cancellationToken)
        {
            LeagueDto leagueDto;
            if (_tierLeague == TierLeague.Challenger)
            {
                leagueDto = await _leagueService.GetChallengerTierLeaguesAsync(_region, _queueType);
            }
            else
            {
                leagueDto = await _leagueService.GetMasterTierLeaguesAsync(_region, _queueType);
            }

            var playerOrTeamIds = new List<long>();
            if (leagueDto?.Entries != null && leagueDto.Entries.Any())
            {
                var ids = leagueDto.Entries
                    .Select(e => e.PlayerOrTeamId)
                    .Where(id => id != null)
                    .Distinct()
                    .ToArray();

                foreach (var id in ids)
                {
                    if (long.TryParse(id, out long idNumber))
                    {
                        playerOrTeamIds.Add(idNumber);
                    }
                }
            }

            Logger.Debug($"{GetParameterString()}: Found {playerOrTeamIds.Count} Player or Team IDs.");
            Logger.Debug($"{string.Join(",", playerOrTeamIds)}");

            return playerOrTeamIds;
        }

        public override string ToString()
        {
            return $"{GetType().Name}{GetParameterString()}>";
        }

        private string GetParameterString()
        {
            return $"<{_region}, {_tierLeague}, {_queueType}>";
        }
    }
}
