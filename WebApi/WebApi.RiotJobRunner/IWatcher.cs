using WebApi.RiotApiClient.Misc;

namespace WebApi.RiotJobRunner
{
    internal interface IWatcher
    {
        void WatchChallengerTierPlayers(Region region);
    }
}
