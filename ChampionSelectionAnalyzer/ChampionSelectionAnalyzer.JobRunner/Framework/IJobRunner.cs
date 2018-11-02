using System;
using System.Collections.Generic;

namespace ChampionSelectionAnalyzer.JobRunner.Framework
{
    internal interface IJobRunner
    {
        bool IsRunning { get; }

        void EnqueueJob(IJob job);

        void EnqueueJobs(IEnumerable<IJob> jobs);

        void Start();

        void Start(TimeSpan baseFrequencyInSeconds);

        void Stop();
    }
}
