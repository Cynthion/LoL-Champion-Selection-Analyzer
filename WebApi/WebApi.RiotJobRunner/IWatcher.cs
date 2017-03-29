using System;
using WebApi.RiotApiClient.Misc;
using WebApi.RiotJobRunner.Jobs;

namespace WebApi.RiotJobRunner
{
    internal interface IWatcher
    {
        void WatchHighTierPlayersAsync(Region region, TierLeague tierLeague, TimeSpan interval);
    }
}
