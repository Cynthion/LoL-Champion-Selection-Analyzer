using System;
using System.Threading;
using System.Threading.Tasks;
using NLog;
using WebApi.RiotApiClient.Services.Interfaces;

namespace WebApi.RiotJobRunner.Jobs
{
    internal class MatchlistJob : JobBase
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        private readonly IMatchService _matchService;

        public MatchlistJob(IMatchService matchService)
        {
            _matchService = matchService;
        }

        protected override async Task DoWorkAsync(CancellationToken cancellationToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(10), cancellationToken);
        }

        protected override void OnStarted()
        {
            Logger.Info($"{GetType().Name} started.");
        }

        protected override void OnCancelled()
        {
            Logger.Info($"{GetType().Name} cancelled.");
        }
    }
}
