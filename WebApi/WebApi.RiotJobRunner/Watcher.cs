using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public void WatchChallengerTierPlayers(Region region)
        {
            var queueTypes = Constants.GetRankedQueueTypes();

            var jobs = queueTypes.Select(q => new ChallengerTierLeagueJob(region, q, _leagueService));

            do
            {
                
            } while(_jobRunner.)
        }
    }
}
