using System;
using WebApi.RiotApiClient.Misc;

namespace WebApi.RiotJobRunner
{
    internal interface IWatcher
    {
        void WatchChallengerTierPlayersAsync(Region region, TimeSpan interval);
    }
}
