using System.Threading;
using System.Threading.Tasks;

namespace WebApi.RiotJobRunner.Jobs
{
    internal abstract class JobBase : IJob
    {
        public bool IsAutoLoop { get; }

        protected JobBase(bool isAutoLoop)
        {
            IsAutoLoop = isAutoLoop;
        }

        public async Task RunAsync(CancellationToken cancellationToken)
        {
            OnStarted();

            do
            {
                OnLoopStarted();

                cancellationToken.Register(OnCancelled);
                await DoWorkAsync(cancellationToken);

            } while (IsAutoLoop);
        }

        protected abstract void OnStarted();

        protected abstract void OnLoopStarted();

        protected abstract Task DoWorkAsync(CancellationToken cancellationToken);

        protected abstract void OnCancelled();
    }
}
