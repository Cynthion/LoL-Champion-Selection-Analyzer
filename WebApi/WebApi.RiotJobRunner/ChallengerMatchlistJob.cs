using System;
using System.Threading;
using System.Threading.Tasks;
using NLog;

namespace WebApi.RiotJobRunner
{
    internal class ChallengerMatchlistJob : JobBase
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        public ChallengerMatchlistJob(CancellationToken cancellationToken)
            : base(cancellationToken)
        {

        }

        protected override async Task DoWorkAsync(CancellationToken cancellationToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(10), cancellationToken);
        }

        protected override void OnStarted()
        {
            Logger.Info($"{nameof(ChallengerMatchlistJob)} started.");
        }

        protected override void OnLoopStarted()
        {
            Logger.Info($"{nameof(ChallengerMatchlistJob)} loop started.");
        }

        protected override void OnCancelled()
        {
            Logger.Info($"{nameof(ChallengerMatchlistJob)} cancelled.");
        }
    }
}
