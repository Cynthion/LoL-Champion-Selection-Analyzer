﻿using System;
using System.Threading;
using ChampionSelectionAnalyzer.RiotApiClient.Misc;
using ChampionSelectionAnalyzer.RiotApiClient.Misc.Interfaces;
using ChampionSelectionAnalyzer.RiotApiClient.Services;
using ChampionSelectionAnalyzer.RiotApiClient.Services.Interfaces;
using Lamar;
using NLog;

namespace ChampionSelectionAnalyzer.JobRunner
{
    internal class Program
    {
        private static readonly ILogger Logger = LogManager.GetLogger(nameof(Program));

        public static void Main(string[] args)
        {
            var cts = new CancellationTokenSource();

            try
            {
                Logger.Log(LogLevel.Info, "Setting up...");

                RavenDb.CreateDatabaseIfNotExists();

                var container = SetupIoC();
                var configuration = container.GetInstance<IJobRunnerConfiguration>();
                var leagueService = container.GetInstance<ILeagueService>();
                var summonerService = container.GetInstance<ISummonerService>();

                Logger.Log(LogLevel.Info, "Setup completed.");

                new RiotPollingService(configuration, leagueService, summonerService).ExecutePolling(cts.Token);

                Console.ReadLine();
            }
            catch (Exception e)
            {
                Logger.Log(LogLevel.Error, e.Message, "Unexpected termination due to an error;");

                Console.ReadLine();
            }
            finally
            {
                cts.Cancel();
            }
        }

        private static Container SetupIoC()
        {
            return new Container(c =>
            {
                c.For<IJobRunnerConfiguration>().Use(_ =>
                    new JobRunnerConfiguration
                    {
                        PolledRegions = new []{ Region.EUW },
                        PolledTierLeagues = new []{ TierLeague.Challenger, TierLeague.Master },
                        PolledQueueTypes = new []{ QueueType.RANKED_SOLO_5x5 },
                        LeaguePollingIntervalInSeconds = 120,
                        SummonerIdsPollingIntervalInSeconds = 120,
                    });

                c.For<IApiKey>().Use(RiotApiKey.CreateFromFile());

                c.For<IWebService>().Use<RiotWebService>();
                c.For<ILeagueService>().Use<LeagueService>();
                c.For<ISummonerService>().Use<SummonerService>();
                c.For<IMatchService>().Use<MatchService>();
            });
        }
    }
}