using System.Collections.Generic;
using ChampionSelectionAnalyzer.RiotApiClient.Misc;

namespace ChampionSelectionAnalyzer.JobRunner
{
    public class JobRunnerConfiguration : IJobRunnerConfiguration
    {
        public IEnumerable<Region> PolledRegions { get; set; }

        public IEnumerable<TierLeague> PolledTierLeagues { get; set; }

        public IEnumerable<QueueType> PolledQueueTypes { get; set; }

        public int LeaguePollingIntervalInSeconds { get; set; }

        public int SummonerIdsPollingIntervalInSeconds { get; set; }
    }
}
