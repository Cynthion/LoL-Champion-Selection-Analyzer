using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ChampionSelectionAnalyzer.JobRunner.Framework;
using ChampionSelectionAnalyzer.JobRunner.Jobs;
using ChampionSelectionAnalyzer.RiotApiClient.Services.Interfaces;
using NLog;

namespace ChampionSelectionAnalyzer.JobRunner
{
    internal class RiotPollingService
    {
        private static readonly ILogger Logger = LogManager.GetLogger(nameof(RiotPollingService));

        private readonly IJobRunnerConfiguration _configuration;
        private readonly ILeagueService _leagueService;
        private readonly IJobRunner _webRequestJobRunner = new Framework.JobRunner();
        private readonly IJobRunner _databaseJobRunner = new Framework.JobRunner();

        public RiotPollingService(
            IJobRunnerConfiguration configuration,
            ILeagueService leagueService)
        {
            _configuration = configuration;
            _leagueService = leagueService;
        }

        public void ExecutePolling(CancellationToken cancellationToken)
        {
            cancellationToken.Register(OnStopped);

            PollLeaguesAsync(cancellationToken);

            _webRequestJobRunner.Start();
        }

        private async void PollLeaguesAsync(CancellationToken cancellationToken)
        {
            var leagueJobs = _configuration.PolledRegions
                .SelectMany(r => _configuration.PolledTierLeagues
                    .SelectMany(t => _configuration.PolledQueueTypes
                        .Select(q =>
                            new LeagueJob(r, t, q, _leagueService)
                        )))
                .ToArray();

            do
            {
                _webRequestJobRunner.EnqueueJobs(leagueJobs);

                try
                {
                    await Task.Delay(TimeSpan.FromSeconds(_configuration.LeaguePollingIntervalInSeconds), cancellationToken);
                }
                catch (TaskCanceledException)
                { }
            } while (!cancellationToken.IsCancellationRequested);
        }

        private void OnStopped()
        {
            _webRequestJobRunner.Stop();
            _databaseJobRunner.Stop();
        }
    }
}