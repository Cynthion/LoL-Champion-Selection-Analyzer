using System;
using System.Linq;
using System.Threading.Tasks;
using NLog;
using WebApi.RiotApiClient;
using WebApi.RiotApiClient.Misc;
using WebApi.RiotApiClient.Services.Interfaces;
using WebApi.RiotJobRunner.Jobs;

namespace WebApi.RiotJobRunner
{
    // TODO remove code duplication

    internal class Watcher
    {
        private const int MaxSummonersPerRegion = 1000; 
        private const int MaxMatchesPerRegion = 10000;      // #summoners * 10
        private const int MaxMatchupsPerRegion = 900000;    // #matches * 90

        private readonly IJobRunner _jobRunner;
        private readonly ILeagueService _leagueService;
        private readonly IMatchService _matchService;
        private readonly IWebApiService _webApiService;

        public Watcher(
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
                var leagueEntriesCount = (await _webApiService.GetLeagueEntriesAsync()).Count();
                if (leagueEntriesCount < MaxSummonersPerRegion)
                {
                    _jobRunner.EnqueueJobs(jobs);
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
                var leagueEntries = await _webApiService.GetLeagueEntriesAsync();
                foreach (var leagueEntry in leagueEntries)
                {
                    var region = (Region)Enum.Parse(typeof(Region), leagueEntry.Region);
                    var summonerId = leagueEntry.PlayerOrTeamId;

                    var job = new MatchListJob(region, summonerId, rankedQueues, seasons, _matchService, _webApiService);
                    _jobRunner.EnqueueJob(job);
                }

                await Task.Delay(interval);

            } while (_jobRunner.IsRunning);
        }

        //public async void WatchMatchupsAsync(Region region, TimeSpan interval)
        //{
        //    var regionMatchIds = GetRegionMatchIds(region);
        //    var regionMatchups = GetRegionMatchups(region);

        //    do
        //    {
        //        if (!regionMatchIds.IsEmpty
        //            && regionMatchIds.TryDequeue(out long matchId)
        //            && regionMatchups.Count < MaxMatchupsPerRegion)
        //        {
        //            var job = new MatchJob(region, matchId, _matchService, matchups => EnqueueMatchups(region, matchups));
        //            _jobRunner.EnqueueJob(job);
        //        }

        //        await Task.Delay(interval);

        //    } while (_jobRunner.IsRunning);
        //}
    }
}
