using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NLog;
using WebApi.RiotJobRunner.Jobs;

namespace WebApi.RiotJobRunner
{
    internal class JobRunner : IJobRunner
    {
        public bool IsRunning => _cts != null && !_cts.IsCancellationRequested;

        public CancellationToken CancellationToken => _cts.Token;

        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        private readonly ConcurrentQueue<IJob> _jobQueue;
        private CancellationTokenSource _cts;

        public JobRunner()
        {
            _jobQueue = new ConcurrentQueue<IJob>();
        }

        public void EnqueueJob(IJob job)
        {
            _jobQueue.Enqueue(job);

            Logger.Info($"{job} put to queue.");
        }

        public void EnqueueJobs(IEnumerable<IJob> jobs)
        {
            foreach (var job in jobs)
            {
                EnqueueJob(job);
            }
        }

        public async void Start(TimeSpan baseFrequency)
        {
            // TODO parallelize execution
            _cts = new CancellationTokenSource();

            Logger.Info($"{GetType().Name} started...");

            do
            {
                if (!_jobQueue.IsEmpty && _jobQueue.TryDequeue(out IJob job))
                {
                    try
                    {
                        await job.RunAsync(_cts.Token);
                    }
                    catch (Exception e)
                    {
                        Logger.Error($"Error during execution of {job}:\n{e}");
                    }

                }

                await Task.Delay(baseFrequency, _cts.Token);

            } while (IsRunning);
        }

        public void Stop()
        {
            _cts?.Cancel();

            Logger.Info($"{GetType().Name} stopped...");
        }
    }
}
