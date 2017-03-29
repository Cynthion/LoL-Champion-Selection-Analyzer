using System.Threading;
using System.Threading.Tasks;

namespace WebApi.RiotJobRunner.Jobs
{
    internal interface IJob
    {
        Task RunAsync(CancellationToken cancellationToken);
    }
}
