using System.Threading;
using System.Threading.Tasks;
using NLog;

namespace WebApi.RiotJobRunner.Jobs
{
    public abstract class JobBase : IJob
    {
        public Task RunAsync(CancellationToken cancellationToken)
        {
            OnStarted();

            cancellationToken.Register(OnCancelled);

            return DoWorkAsync(cancellationToken);
        }

        protected abstract Task DoWorkAsync(CancellationToken cancellationToken);

        protected virtual void OnStarted()
        {
            LogManager.GetLogger(GetType().Name) .Info($"{this} started.");
        }

        protected virtual void OnCancelled()
        {
            LogManager.GetLogger(GetType().Name).Info($"{this} cancelled.");
        }
    }
}
