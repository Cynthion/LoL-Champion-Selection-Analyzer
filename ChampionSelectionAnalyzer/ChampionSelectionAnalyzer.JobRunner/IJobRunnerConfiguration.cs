using System.Collections.Generic;
using ChampionSelectionAnalyzer.RiotApiClient.Misc;

namespace ChampionSelectionAnalyzer.JobRunner
{
    internal interface IJobRunnerConfiguration
    {
        int LeaguePollingIntervalInSeconds { get; set; }

        IEnumerable<Region> PolledRegions { get; set; }

        IEnumerable<TierLeague> PolledTierLeagues { get; set; }

        IEnumerable<QueueType> PolledQueueTypes { get; set; }

    }
}
