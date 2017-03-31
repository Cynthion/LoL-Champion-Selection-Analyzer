using System;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using StructureMap;
using WebApi.RiotApiClient.Misc;
using WebApi.RiotApiClient.Misc.Interfaces;
using WebApi.RiotApiClient.Services;
using WebApi.RiotApiClient.Services.Interfaces;
using WebApi.RiotJobRunner.Jobs;

namespace WebApi.RiotJobRunner
{
    internal class Program
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        public static void Main(string[] args)
        {
            var services = new ServiceCollection()
                .AddSingleton<IApiKey>(c => {
                    var apiKey = RiotApiKey.CreateFromFile();
                    Logger.Info($"Used API Key:\n{apiKey}");
                    return apiKey;
                })
                .AddSingleton<IWebService>(c => RiotWebService.Instance)
                .AddSingleton<ILeagueService, LeagueService>()
                .AddSingleton<IMatchService, MatchService>();
            var container = new Container();

            container.Configure(config => config.Populate(services));

            var webJobsRunner = new JobRunner();
            var databaseJobsRunner = new JobRunner();

            var watcher = new Watcher(
                webJobsRunner, 
                databaseJobsRunner,
                container.GetInstance<ILeagueService>(),
                container.GetInstance<IMatchService>());

            watcher.WatchHighTierPlayersAsync(Region.EUW, TierLeague.Challenger, TimeSpan.FromDays(1));
            //watcher.WatchHighTierPlayersAsync(Region.EUW, TierLeague.Master, TimeSpan.FromDays(1));

            //watcher.WatchHighTierPlayersAsync(Region.NA, TierLeague.Challenger, TimeSpan.FromDays(0.5));
            //watcher.WatchHighTierPlayersAsync(Region.NA, TierLeague.Master, TimeSpan.FromDays(0.5));

            watcher.WatchMatchlistsAsync(Region.EUW, TimeSpan.FromSeconds(10));
            //watcher.WatchMatchlistsAsync(Region.NA, TimeSpan.FromSeconds(10));

            //watcher.WatchMatchupsAsync(Region.EUW, TimeSpan.FromSeconds(1));
            //watcher.WatchMatchupsAsync(Region.NA, TimeSpan.FromSeconds(1));

            try
            {
                webJobsRunner.Start(TimeSpan.FromSeconds(1));
                databaseJobsRunner.Start(TimeSpan.FromSeconds(1)); // TODO decrease
                Console.ReadLine();
            }
            finally
            {
                webJobsRunner.Stop();
                databaseJobsRunner.Stop();       
            }
        }
    }
}