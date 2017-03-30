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

        // TODO add upper limit for queue before new items can be added
        private readonly ConcurrentDictionary<Region, ConcurrentQueue<long>> _regionSummonerIds;
        private readonly ConcurrentDictionary<Region, ConcurrentQueue<long>> _regionMatchIds;

        public Watcher(
            IJobRunner jobRunner,
            ILeagueService leagueService,
            IMatchService matchService)
        {
            _jobRunner = jobRunner;
            _leagueService = leagueService;
            _matchService = matchService;

            _regionSummonerIds = new ConcurrentDictionary<Region, ConcurrentQueue<long>>();
            _regionMatchIds = new ConcurrentDictionary<Region, ConcurrentQueue<long>>();
        }

        public async void WatchHighTierPlayersAsync(Region region, TierLeague tierLeague, TimeSpan interval)
        {
            var rankedQueues = GameConstants.GetRankedQueueTypes();
            var regionSummonerIds = GetRegionSummonerIds(region);

            var jobs = rankedQueues
                .Select(queue => new TierLeagueJob(region, tierLeague, queue, _leagueService, summonerIds => EnqueueSummonerIds(region, summonerIds)))
                .ToArray();

            await RunJobs(jobs, interval, () => regionSummonerIds.Count < 1000);
        }

        public async void WatchMatchlistsAsync(Region region, TimeSpan interval)
        {
            var rankedQueues = GameConstants.GetRankedQueueTypes();
            var seasons = GameConstants.GetCurrentSeasons();

            var regionSummonerIds = GetRegionSummonerIds(region);
            var regionMatchIds = GetRegionMatchIds(region);

            do
            {
                if (!regionSummonerIds.IsEmpty 
                    && regionSummonerIds.TryDequeue(out long summonerId)
                    && regionMatchIds.Count < 3000)
                {
                    // TODO only get matchlist for last week or so
                    var job = new MatchListJob(region, summonerId, rankedQueues, seasons, _matchService, matchIds => EnqueueMatchIds(region, matchIds));
                    _jobRunner.EnqueueJob(job);
                }

                await Task.Delay(interval);

            } while (_jobRunner.IsRunning);
        }

        private async Task RunJobs(ICollection<IJob> jobs, TimeSpan interval, Func<bool> condition = null)
        {
            do
            {
                if (condition != null && condition())
                {
                    _jobRunner.EnqueueJobs(jobs);
                }

                await Task.Delay(interval);

            } while (_jobRunner.IsRunning);
        }

        private void EnqueueSummonerIds(Region region, IEnumerable<long> summonerIds)
        {
            var regionSummonerIds = GetRegionSummonerIds(region);

            foreach (var summonerId in summonerIds)
            {
                // TODO don't enqueue the same IDs over and over again (or don't process them)
                regionSummonerIds.Enqueue(summonerId);
            }

            Logger.Info($"{region} Summoner ID Queue contains {regionSummonerIds.Count} items.");
        }

        private void EnqueueMatchIds(Region region, IEnumerable<long> matchIds)
        {
            var regionMatchIds = GetRegionMatchIds(region);

            foreach (var matchId in matchIds)
            {
                // TODO don't enqueue the same IDs over and over again (or don't process them)
                regionMatchIds.Enqueue(matchId);
            }

            Logger.Info($"{region} Match ID Queue contains {regionMatchIds.Count} items.");
        }

        private ConcurrentQueue<long> GetRegionSummonerIds(Region region)
        {
            return _regionSummonerIds.GetOrAdd(region, new ConcurrentQueue<long>());
        }

        private ConcurrentQueue<long> GetRegionMatchIds(Region region)
        {
            return _regionMatchIds.GetOrAdd(region, new ConcurrentQueue<long>());
        }
    }
}
