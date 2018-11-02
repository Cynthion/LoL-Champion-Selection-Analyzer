using System.Collections.Generic;
using ChampionSelectionAnalyzer.RiotApiClient.Misc;

namespace ChampionSelectionAnalyzer.JobRunner
{
    internal interface IJobRunnerConfiguration
    {
        IEnumerable<Region> PolledRegions { get; set; }

        IEnumerable<TierLeague> PolledTierLeagues { get; set; }

        IEnumerable<QueueType> PolledQueueTypes { get; set; }

        // from Riot API
        int LeaguePollingIntervalInSeconds { get; set; }

        // from local DB
        int SummonerIdsPollingIntervalInSeconds { get; set; }
    }
}
