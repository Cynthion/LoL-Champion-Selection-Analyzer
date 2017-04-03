using System;
using System.Linq;
using System.Threading.Tasks;
using NLog;
using WebApi.Model.Entities.League;
using WebApi.Model.Enums;
using WebApi.RiotApiClient;
using WebApi.RiotApiClient.Services.Interfaces;
using WebApi.RiotJobRunner.Jobs;

namespace WebApi.RiotJobRunner
{
    // TODO remove code duplication

    internal class JobConfigurator
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        private const int MaxLeagueEntries = 5000; 
        private const int MaxMatchReferences = 100000;  // 20 matches per summoner

        private readonly IJobRunner _jobRunner;
        private readonly ILeagueService _leagueService;
        private readonly IMatchService _matchService;
        private readonly IWebApiService _webApiService;

        public JobConfigurator(
            IJobRunner jobRunner,
            ILeagueService leagueService,
            IMatchService matchService,
            IWebApiService webApiService)
        {
            _jobRunner = jobRunner;
            _leagueService = leagueService;
            _matchService = matchService;
            _webApiService = webApiService;
        }

        public async void PollLeagueEntriesAsync(Region region, TierLeague tierLeague, TimeSpan interval)
        {
            var rankedQueues = GameConstants.GetRankedQueueTypes();

            var jobs = rankedQueues
                .Select(queue => new TierLeagueJob(region, tierLeague, queue, _leagueService, _webApiService))
                .ToArray();

            do
            {
                try
                {
                    var summonerLeagueEntryCount = await _webApiService.GetSummonerLeagueEntryCountAsync();
                    Logger.Info($"{nameof(SummonerLeagueEntry)} Count: {summonerLeagueEntryCount}");

                    if (summonerLeagueEntryCount < MaxLeagueEntries)
                    {
                        _jobRunner.EnqueueJobs(jobs);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                }

                await Task.Delay(interval);

            } while (_jobRunner.IsRunning);
        }

        public async void PollMatchlistsAsync(TimeSpan interval)
        {
            var rankedQueues = GameConstants.GetRankedQueueTypes();
            var seasons = GameConstants.GetCurrentSeasons();

            do
            {
                try
                {
                    var summonerLeagueEntries = await _webApiService.GetSummonerLeagueEntriesAsync();
                    var matchReferencesCount = await _webApiService.GetMatchReferenceCountAsync();
                    Logger.Info($"Match References Count: {matchReferencesCount}");

                    if (matchReferencesCount < MaxMatchReferences)
                    {
                        foreach (var summonerLeagueEntry in summonerLeagueEntries)
                        {
                            var job = new MatchListJob(summonerLeagueEntry.Region, summonerLeagueEntry.PlayerId, rankedQueues, seasons, _matchService, _webApiService);
                            _jobRunner.EnqueueJob(job);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                }

                await Task.Delay(interval);

            } while (_jobRunner.IsRunning);
        }

        public async void PollMatchesAsync(TimeSpan interval)
        {
            do
            {
                try
                {
                    var matchReferences = await _webApiService.GetMatchReferencesAsync();
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                }

                await Task.Delay(interval);

            } while (_jobRunner.IsRunning);
        }
    }
}
