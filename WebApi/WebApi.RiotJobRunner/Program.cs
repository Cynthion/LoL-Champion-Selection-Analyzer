using System;
using System.Collections.Generic;
using System.Threading;
using NLog;

namespace WebApi.RiotJobRunner
{
    internal class Program
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        private static void Main(string[] args)
        {
            Logger.Info("Riot Job Runner started...");

            var jobsTokenSource = new CancellationTokenSource();

            var jobs = new List<IJob>
            {
                new ChallengerMatchlistJob(jobsTokenSource.Token)
            };

            jobs.ForEach(j => j.RunAsync());

            Console.ReadLine();
            Logger.Info("Riot Job Runner stopped...");

            jobsTokenSource.Cancel();
            Console.ReadLine();
        }
    }
}