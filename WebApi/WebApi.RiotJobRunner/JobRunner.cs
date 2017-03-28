using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NLog;
using WebApi.RiotJobRunner.Jobs;

namespace WebApi.RiotJobRunner
{
    internal class JobRunner : IJobRunner
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        private readonly ICollection<IJob> _jobs;
        private readonly CancellationTokenSource _cts;

        public JobRunner()
        {
            _jobs = new List<IJob>();
            _cts = new CancellationTokenSource();
        }

        public void RegisterJob(IJob job)
        {
            _jobs.Add(job);
            Logger.Info($"{job.GetType().Name} registered.");
        }

        public void Run()
        {
            Logger.Info("Job Runner started...");

            var jobTasks = _jobs.Select(job => job.RunAsync(_cts.Token));

            Task.WhenAll(jobTasks).Wait();
        }

        public void Stop()
        {
            Logger.Info("Job Runner stopped...");

            _cts.Cancel();
        }
    }
}
