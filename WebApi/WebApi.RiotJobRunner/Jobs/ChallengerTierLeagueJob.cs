using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NLog;
using WebApi.RiotApiClient.Misc;
using WebApi.RiotApiClient.Services.Interfaces;

namespace WebApi.RiotJobRunner.Jobs
{
    internal class ChallengerTierLeagueJob : JobBase
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        private readonly Region _region;
        private readonly string _queueType;
        private readonly ILeagueService _leagueService;

        public ChallengerTierLeagueJob(
            Region region,
            string queueType,
            ILeagueService leagueService)
        {
            _region = region;
            _queueType = queueType;
            _leagueService = leagueService;
        }

        protected override async Task DoWorkAsync(CancellationToken cancellationToken)
        {
            var leagueDto = await _leagueService.GetChallengerTierLeaguesAsync(_region, _queueType);

            var playerOrTeamIds = leagueDto.Entries.Select(e => e.PlayerOrTeamId);
        }

        protected override void OnStarted()
        {
            Logger.Info($"{nameof(MatchlistJob)} started.");
        }

        protected override void OnCancelled()
        {
            Logger.Info($"{nameof(MatchlistJob)} cancelled.");
        }
    }
}
