using System;
using System.Linq;
using System.Threading.Tasks;
using WebApi.RiotApiClient;
using WebApi.RiotApiClient.Misc;
using WebApi.RiotApiClient.Services.Interfaces;
using WebApi.RiotJobRunner.Jobs;

namespace WebApi.RiotJobRunner
{
    internal class Watcher : IWatcher
    {
        private readonly IJobRunner _jobRunner;
        private readonly ILeagueService _leagueService;

        public Watcher(
            IJobRunner jobRunner,
            ILeagueService leagueService)
        {
            _jobRunner = jobRunner;
            _leagueService = leagueService;
        }

        public async void WatchChallengerTierPlayersAsync(Region region, TimeSpan interval)
        {
            var queueTypes = GameConstants.GetRankedQueueTypes();

            var jobs = queueTypes.Select(q => new ChallengerTierLeagueJob(region, q, _leagueService)).ToArray();

            do
            {
                _jobRunner.EnqueueJobs(jobs);

                await Task.Delay(interval);

            } while (_jobRunner.IsRunning);
        }
    }
}
