using System.Threading;
using System.Threading.Tasks;

namespace WebApi.RiotJobRunner
{
    internal abstract class JobBase : IJob
    {
        public bool IsAutoLoop { get; }

        protected readonly CancellationToken CancellationToken;

        protected JobBase(CancellationToken cancellationToken, bool isAutoLoop = true)
        {
            cancellationToken.Register(OnCancelled);

            CancellationToken = cancellationToken;
            IsAutoLoop = isAutoLoop;
        }

        public async Task RunAsync()
        {
            OnStarted();

            do
            {
                OnLoopStarted();
                await DoWorkAsync(CancellationToken);
            } while (IsAutoLoop);
        }

        protected abstract void OnStarted();

        protected abstract void OnLoopStarted();

        protected abstract Task DoWorkAsync(CancellationToken cancellationToken);

        protected abstract void OnCancelled();
    }
}
