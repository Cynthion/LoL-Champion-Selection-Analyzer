using System.Collections.Generic;
using System.Threading;
using ChampionSelectionAnalyzer.JobRunner.Framework;

namespace ChampionSelectionAnalyzer.JobRunner
{
    internal class RiotPollingService
    {
        private readonly IEnumerable<IJobRunner> _jobRunners = new List<IJobRunner>();

        public void ExecutePolling(CancellationToken cancellationToken)
        {
            cancellationToken.Register(OnStopped);
        }

        private void OnStopped()
        {
            foreach (var jobRunner in _jobRunners)
            {
                jobRunner.Stop();
            }
        }
    }
}