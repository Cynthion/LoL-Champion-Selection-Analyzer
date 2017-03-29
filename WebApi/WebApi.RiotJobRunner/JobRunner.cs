using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NLog;
using WebApi.RiotApiClient.Misc.Exceptions;
using WebApi.RiotJobRunner.Jobs;

namespace WebApi.RiotJobRunner
{
    internal class JobRunner : IJobRunner
    {
        public bool IsRunning => _cts != null && !_cts.IsCancellationRequested;

        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        private readonly Queue<IJob> _jobQueue;
        private CancellationTokenSource _cts;

        public JobRunner()
        {
            _jobQueue = new Queue<IJob>();
        }

        public void EnqueueJob(IJob job)
        {
            _jobQueue.Enqueue(job);

            Logger.Info($"{job.GetType().Name} put to queue.");
        }

        public void EnqueueJobs(IEnumerable<IJob> jobs)
        {
            foreach (var job in jobs)
            {
                EnqueueJob(job);
            }
        }

        public async void Start(TimeSpan interval)
        {
            _cts = new CancellationTokenSource();

            Logger.Info($"{GetType().Name} started...");

            do
            {
                await Task.Delay(interval);

                if (_jobQueue.Any())
                {
                    var job = _jobQueue.Dequeue();

                    try
                    {
                        await job.RunAsync(_cts.Token);
                    }
                    catch (RioApiException e)
                    {
                        Logger.Error(e);
                    }
                }

            } while (!_cts.Token.IsCancellationRequested);
        }

        public void Stop()
        {
            _cts?.Cancel();

            Logger.Info($"{GetType().Name} stopped...");
        }
    }
}
