using System;
using Microsoft.Extensions.DependencyInjection;
using StructureMap;
using WebApi.RiotApiClient.Misc;
using WebApi.RiotApiClient.Services;
using WebApi.RiotApiClient.Services.Interfaces;

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

            var watcher = new Watcher(jobRunner, container.GetInstance<ILeagueService>());
            watcher.WatchChallengerTierPlayersAsync(Region.EUW, TimeSpan.FromDays(0.5));
            watcher.WatchChallengerTierPlayersAsync(Region.NA, TimeSpan.FromDays(0.5));

            Console.ReadLine();
            jobRunner.Stop();
        }
    }
}