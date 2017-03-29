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

    internal class TierLeagueJob : JobBase<IEnumerable<string>>
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
            Action<IEnumerable<string>> resultAction)
            : base(resultAction)
        {
            _region = region;
            _tierLeague = tierLeague;
            _queueType = queueType;
            _leagueService = leagueService;
        }

        protected override async Task<IEnumerable<string>> DoWorkAsync(CancellationToken cancellationToken)
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

            var playerOrTeamIds = new List<string>();
            if (leagueDto?.Entries != null && leagueDto.Entries.Any())
            {
                var ids = leagueDto.Entries
                    .Select(e => e.PlayerOrTeamId)
                    .Where(id => id != null)
                    .Distinct();

                playerOrTeamIds.AddRange(ids);
            }

            Logger.Debug($"Found {playerOrTeamIds.Count} Player or Team IDs.");

            return playerOrTeamIds;
        }

        public override string ToString()
        {
            return $"{GetType().Name}<{_region}, {_tierLeague}, {_queueType}>";
        }
    }
}
