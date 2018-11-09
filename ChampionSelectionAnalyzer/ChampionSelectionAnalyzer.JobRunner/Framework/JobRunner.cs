using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NLog;

namespace ChampionSelectionAnalyzer.JobRunner.Framework
{
    internal class JobRunner : IJobRunner
    {
        public bool IsRunning => _cts != null && !_cts.IsCancellationRequested;

        private static readonly ILogger Logger = LogManager.GetLogger(nameof(JobRunner));

        private readonly ConcurrentQueue<IJob> _jobQueue;
        private CancellationTokenSource _cts;

        public JobRunner()
        {
            _jobQueue = new ConcurrentQueue<IJob>();
        }

        public void EnqueueJob(IJob job)
        {
            _jobQueue.Enqueue(job);

            LogManager.GetLogger(GetType().Name).Log(LogLevel.Info, $"Enqueued { job }.");
        }

        public void EnqueueJobs(IEnumerable<IJob> jobs)
        {
            foreach (var job in jobs)
            {
                EnqueueJob(job);
            }
        }

        public async void Start()
        {
            _cts = new CancellationTokenSource();

            Logger.Log(LogLevel.Info, $"{ GetType().Name } started...");

            do
            {
                if (!_jobQueue.IsEmpty && _jobQueue.TryDequeue(out var job))
                {
                    try
                    {
                        await job.RunAsync(_cts.Token);
                    }
                    catch (Exception e)
                    {
                        Logger.Log(LogLevel.Error, $"Error during execution of { job }:\n{ e }");
                    }
                }
            } while (IsRunning);
        }

        public void Stop()
        {
            _cts?.Cancel();

            Logger.Log(LogLevel.Info, $"{ GetType().Name } stopped...");
        }
    }
}
