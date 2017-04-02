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
                .AddSingleton<IWebApiService, WebApiService>()
                .AddSingleton<ILeagueService, LeagueService>()
                .AddSingleton<IMatchService, MatchService>();
            var container = new Container();

            container.Configure(config => config.Populate(services));

            var jobRunner = new JobRunner();

            var configurator = new JobConfigurator(
                jobRunner, 
                container.GetInstance<ILeagueService>(),
                container.GetInstance<IMatchService>(),
                container.GetInstance<IWebApiService>());

            configurator.PollLeagueEntriesAsync(Region.EUW, TierLeague.Challenger, TimeSpan.FromDays(1));
            //watcher.WatchHighTierPlayersAsync(Region.EUW, TierLeague.Master, TimeSpan.FromDays(1));

            // TODO enable per-region
            configurator.PollMatchlistsAsync(TimeSpan.FromHours(1));
            //watcher.WatchHighTierPlayersAsync(Region.NA, TierLeague.Master, TimeSpan.FromDays(0.5));

            //watcher.PollMatchlistsAsync(Region.EUW, TimeSpan.FromSeconds(10));
            //watcher.PollMatchlistsAsync(Region.NA, TimeSpan.FromSeconds(10));

            //watcher.WatchMatchupsAsync(Region.EUW, TimeSpan.FromSeconds(1));
            //watcher.WatchMatchupsAsync(Region.NA, TimeSpan.FromSeconds(1));

            try
            {
                jobRunner.Start(TimeSpan.FromSeconds(1));
                Console.ReadLine();
            }
            finally
            {
                jobRunner.Stop();
            }
        }
    }
}