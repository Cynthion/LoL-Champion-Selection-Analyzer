using System.Threading.Tasks;

namespace WebApi.RiotJobRunner
{
    internal interface IJob
    {
        bool IsAutoLoop { get; }

        Task RunAsync();
    }
}
