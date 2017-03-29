using System;
using Microsoft.Extensions.DependencyInjection;
using StructureMap;
using WebApi.RiotApiClient.Misc;
using WebApi.RiotApiClient.Services;
using WebApi.RiotApiClient.Services.Interfaces;
using WebApi.RiotJobRunner.Jobs;

namespace WebApi.RiotJobRunner
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var services = new ServiceCollection()
                .AddSingleton<IWebService>(c => RiotWebService.Instance)
                .AddSingleton<ILeagueService, LeagueService>()
                .AddSingleton<IMatchService, MatchService>();
            var container = new Container();

            container.Configure(config => config.Populate(services));

            var jobRunner = new JobRunner();
            jobRunner.Start(TimeSpan.FromSeconds(1));

            var watcher = new Watcher(jobRunner, 
                container.GetInstance<ILeagueService>(),
                container.GetInstance<IMatchService>());

            watcher.WatchHighTierPlayersAsync(Region.EUW, TierLeague.Challenger, TimeSpan.FromDays(0.5));
            watcher.WatchHighTierPlayersAsync(Region.EUW, TierLeague.Master, TimeSpan.FromDays(0.5));

            watcher.WatchHighTierPlayersAsync(Region.NA, TierLeague.Challenger, TimeSpan.FromDays(0.5));
            watcher.WatchHighTierPlayersAsync(Region.NA, TierLeague.Master, TimeSpan.FromDays(0.5));

            watcher.WatchMatchlistsAsync(Region.EUW, TimeSpan.FromSeconds(10));
            watcher.WatchMatchlistsAsync(Region.NA, TimeSpan.FromSeconds(10));

            Console.ReadLine();
            jobRunner.Stop();
        }
    }
}