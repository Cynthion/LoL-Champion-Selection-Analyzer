using System.Threading;
using System.Threading.Tasks;

namespace WebApi.RiotJobRunner.Jobs
{
    public abstract class JobBase : IJob
    {
        public async Task RunAsync(CancellationToken cancellationToken)
        {
            OnStarted();

            cancellationToken.Register(OnCancelled);
            await DoWorkAsync(cancellationToken);
        }

        protected abstract void OnStarted();

        protected abstract Task DoWorkAsync(CancellationToken cancellationToken);

        protected abstract void OnCancelled();
    }
}
