using System.Threading;
using System.Threading.Tasks;

namespace WebApi.RiotJobRunner.Jobs
{
    internal interface IJob
    {
        bool IsAutoLoop { get; }

        Task RunAsync(CancellationToken cancellationToken);
    }
}
