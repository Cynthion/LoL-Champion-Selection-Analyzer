using System;
using Microsoft.Extensions.DependencyInjection;
using StructureMap;
using WebApi.RiotApiClient.Services;
using WebApi.RiotApiClient.Services.Interfaces;
using WebApi.RiotJobRunner.Jobs;

namespace WebApi.RiotJobRunner
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var services = new ServiceCollection()
                .AddSingleton<IWebService>(c => RiotWebService.Instance)
                .AddSingleton<IMatchService, MatchService>();
            var container = new Container();

            container.Configure(config => config.Populate(services));

            var jobRunner = new JobRunner();
            jobRunner.RegisterJob(container.GetInstance<MatchlistJob>());

            jobRunner.Run();

            Console.ReadLine();
            jobRunner.Stop();
        }
    }
}