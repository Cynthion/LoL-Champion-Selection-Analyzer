using WebApi.RiotJobRunner.Jobs;

namespace WebApi.RiotJobRunner
{
    internal interface IJobRunner
    {
        void RegisterJob(IJob job);

        void Run();

        void Stop();
    }
}
