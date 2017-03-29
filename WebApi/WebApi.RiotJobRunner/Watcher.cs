using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog;
using WebApi.RiotApiClient;
using WebApi.RiotApiClient.Misc;
using WebApi.RiotApiClient.Services.Interfaces;
using WebApi.RiotJobRunner.Jobs;

namespace WebApi.RiotJobRunner
{
    internal class Watcher : IWatcher
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        private readonly IJobRunner _jobRunner;
        private readonly ILeagueService _leagueService;
        private readonly IMatchService _matchService;
        private readonly ConcurrentDictionary<Region, ConcurrentQueue<string>> _regionSummonerIds;

        public Watcher(
            IJobRunner jobRunner,
            ILeagueService leagueService,
            IMatchService matchService)
        {
            _jobRunner = jobRunner;
            _leagueService = leagueService;
            _matchService = matchService;

            _regionSummonerIds = new ConcurrentDictionary<Region, ConcurrentQueue<string>>();
        }

        public async void WatchHighTierPlayersAsync(Region region, TierLeague tierLeague, TimeSpan interval)
        {
            var rankedQueues = GameConstants.GetRankedQueueTypes();

            var jobs = rankedQueues
                .Select(queue => new TierLeagueJob(region, tierLeague, queue, _leagueService, result => EnqueueSummonerIds(region, result)))
                .ToArray();

            await RunJobs(interval, jobs);
        }

        //public async void WatchMatchlistsAsync(Region region, TimeSpan interval)
        //{
        //    var rankedQueues = GameConstants.GetRankedQueueTypes();
        //    var seasons = new []
        //    {
        //        GameConstants.GetSeasonForYear(DateTime.Now)
        //    };

        //    // TODO only get matchlist for last week or so
        //    //_matchService.GetMatchListAsync(region, null, rankedQueues, seasons)
        //}

        private void EnqueueSummonerIds(Region region, IEnumerable<string> summmonerIds)
        {
            var summonerIds = GetRegionSummonerIds(region);

            foreach (var summonerId in summmonerIds)
            {
                // TODO don't enqueue the same IDs over and over again (or don't process them)
                summonerIds.Enqueue(summonerId);
            }

            Logger.Info($"{region} Summoner ID Queue contains {summonerIds.Count} items.");
        }

        private async Task RunJobs(TimeSpan interval, ICollection<IJob> jobs)
        {
            do
            {
                _jobRunner.EnqueueJobs(jobs);

                await Task.Delay(interval);

            } while (_jobRunner.IsRunning);
        }

        private ConcurrentQueue<string> GetRegionSummonerIds(Region region)
        {
            return _regionSummonerIds.GetOrAdd(region, new ConcurrentQueue<string>());
        }
    }
}
