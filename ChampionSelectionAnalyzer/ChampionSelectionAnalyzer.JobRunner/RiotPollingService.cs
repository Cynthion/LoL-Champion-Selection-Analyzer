using System.Threading;
using ChampionSelectionAnalyzer.JobRunner.Framework;
using NLog;

namespace ChampionSelectionAnalyzer.JobRunner
{
    internal class RiotPollingService
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        private readonly IJobRunnerConfiguration _configuration;
        private readonly IJobRunner _webRequestJobRunner = new Framework.JobRunner();
        private readonly IJobRunner _databaseJobRunner = new Framework.JobRunner();

        public RiotPollingService(IJobRunnerConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ExecutePolling(CancellationToken cancellationToken)
        {
            cancellationToken.Register(OnStopped);
        }

        private void OnStopped()
        {
            _webRequestJobRunner.Stop();
            _databaseJobRunner.Stop();
        }
    }
}