using System;
using System.Collections.Generic;
using WebApi.RiotJobRunner.Jobs;

namespace WebApi.RiotJobRunner
{
    internal interface IJobRunner
    {
        bool IsRunning { get; }

        void EnqueueJob(IJob job);

        void EnqueueJobs(IEnumerable<IJob> jobs);

        void Start(TimeSpan baseFrequency);

        void Stop();
    }
}
