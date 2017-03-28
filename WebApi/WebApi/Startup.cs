using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using WebApi.RiotApiClient.Misc;
using WebApi.RiotApiClient.Misc.Interfaces;
using WebApi.RiotApiClient.Services;
using WebApi.RiotApiClient.Services.Interfaces;
using WebApi.Services;
using WebApi.Services.Interfaces;
using ILogger = NLog.ILogger;

namespace WebApi
{
    public class Startup
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        // TODO use multiple environments https://docs.microsoft.com/en-us/aspnet/core/fundamentals/environments
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services, IServiceProvider serviceProvider)
        {
            // Add framework services.
            services.AddMvc(); // TODO remove, since MVC is not used

            // Database contexts
            //services.AddDbContext<[DbContext]>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Repositories
            //services.AddSingleton<ITodoRepository, TodoRepository>();

            services.AddSingleton<IApiKey>(c => {
                var apiKey = RiotApiKey.CreateFromFile();
                Logger.Info($"Used API Key:\n{apiKey}");
                return apiKey;
            });
            
            // Services
            services.AddSingleton<IWebService>(c => RiotWebService.Instance);
            services.AddSingleton<ISummonerService, SummonerService>();
            services.AddSingleton<IStatsService, StatsService>();
            services.AddSingleton<IMatchService, MatchService>();
            services.AddSingleton<ILeagueService, LeagueService>();

            //services.AddSingleton<IRegionSelector, RegionSelector>();
            services.AddSingleton<ISuggestionService, SuggestionService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc(); // TODO remove, since MVC is not used
        }
    }
}
