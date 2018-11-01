using System;
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
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        public static void Main(string[] args)
        {
            var cts = new CancellationTokenSource();

            try
            {
                SetupIoC();

                new RiotPollingService().ExecutePolling(cts.Token);

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

        private static void SetupIoC()
        {
            var container = new Container(c =>
            {
                c.For<IApiKey>().Use(a =>
                {
                    var apiKey = RiotApiKey.CreateFromFile();
                    Logger.Info($"Using API Key:\n{apiKey}");
                    return apiKey;
                });

                c.For<IWebService>().Use(RiotWebService.Instance);

                c.For<ILeagueService>().Use<LeagueService>();
            });
        }
    }
}