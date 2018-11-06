using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ChampionSelectionAnalyzer.JobRunner.Framework;
using ChampionSelectionAnalyzer.JobRunner.Jobs;
using ChampionSelectionAnalyzer.JobRunner.Jobs.Polling.Database;
using ChampionSelectionAnalyzer.JobRunner.Jobs.Polling.Web;
using ChampionSelectionAnalyzer.RiotApiClient.Services.Interfaces;
using NLog;

namespace ChampionSelectionAnalyzer.JobRunner
{
    internal class RiotPollingService
    {
        private static readonly ILogger Logger = LogManager.GetLogger(nameof(RiotPollingService));

        private readonly IJobRunnerConfiguration _configuration;
        private readonly IJobRunner _databaseJobRunner = new Framework.JobRunner();
        private readonly ILeagueService _leagueService;
        private readonly ISummonerService _summonerService;
        private readonly IJobRunner _webRequestJobRunner = new Framework.JobRunner();

        public RiotPollingService(
            IJobRunnerConfiguration configuration,
            ILeagueService leagueService,
            ISummonerService summonerService)
        {
            _configuration = configuration;
            _leagueService = leagueService;
            _summonerService = summonerService;
        }

        public void ExecutePolling(CancellationToken cancellationToken)
        {
            Logger.Log(LogLevel.Info, "Execute polling...");
            cancellationToken.Register(OnStopped);

            PollLeaguesAsync(cancellationToken);
            //PollSummonersAsync(cancellationToken);
            //PollMatchesAsync(cancellationToken);

            _webRequestJobRunner.Start();
        }

        private async void PollLeaguesAsync(CancellationToken cancellationToken)
        {
            var leagueJobs = _configuration.PolledRegions
                .SelectMany(r => _configuration.PolledTierLeagues
                    .SelectMany(t => _configuration.PolledQueueTypes
                        .Select(q =>
                            new LeagueJob(r, t, q, _leagueService,
                                result => _databaseJobRunner.EnqueueJob(new SaveLeagueJob(result)))
                        )))
                .ToArray();

            do
            {
                _webRequestJobRunner.EnqueueJobs(leagueJobs);

                try
                {
                    await Task.Delay(TimeSpan.FromSeconds(_configuration.LeaguePollingIntervalInSeconds),
                        cancellationToken);
                }
                catch (TaskCanceledException)
                {
                }
            } while (!cancellationToken.IsCancellationRequested);
        }

        private async void PollSummonersAsync(CancellationToken cancellationToken)
        {
            do
            {
                var loadSummonerIdsJobs = _configuration.PolledRegions
                    .Select(region => new LoadSummonerIdsJob(region, summonerIds =>
                    {
                        var summonerJobs = summonerIds.Select(summonerId => new SummonerJob(region, long.Parse(summonerId), _summonerService, summonerDto =>
                            {
                                var saveSummonerJob = new SaveSummonerJob(summonerDto);
                                _databaseJobRunner.EnqueueJob(saveSummonerJob);
                            }));
                        _webRequestJobRunner.EnqueueJobs(summonerJobs);
                    }));
                _databaseJobRunner.EnqueueJobs(loadSummonerIdsJobs);

                try
                {
                    await Task.Delay(TimeSpan.FromSeconds(_configuration.SummonerIdsPollingIntervalInSeconds),
                        cancellationToken);
                }
                catch (TaskCanceledException)
                {
                }
            } while (!cancellationToken.IsCancellationRequested);
        }

        //private async void PollMatchesAsync(CancellationToken cancellationToken)
        //{
        //    do
        //    {

        //    }
        //}

        private void OnStopped()
        {
            _webRequestJobRunner.Stop();
            _databaseJobRunner.Stop();
        }
    }
}