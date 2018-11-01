using System.Threading;
using System.Threading.Tasks;

namespace ChampionSelectionAnalyzer.JobRunner.Framework
{
    internal interface IJob
    {
        Task RunAsync(CancellationToken cancellationToken);
    }
}
