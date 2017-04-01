using System.Threading;
using System.Threading.Tasks;
using NLog;
using WebApi.Model.Dtos.League;
using WebApi.RiotApiClient.Misc;
using WebApi.RiotApiClient.Services.Interfaces;

namespace WebApi.RiotJobRunner.Jobs
{
    internal enum TierLeague
    {
        Challenger,
        Master
    }

    internal class TierLeagueJob : JobBase
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        private readonly Region _region;
        private readonly TierLeague _tierLeague;
        private readonly string _queueType;
        private readonly ILeagueService _leagueService;
        private readonly IWebApiService _webApiService;

        public TierLeagueJob(
            Region region, 
            TierLeague tierLeague,
            string queueType, 
            ILeagueService leagueService,
            IWebApiService webApiService)
        {
            _region = region;
            _tierLeague = tierLeague;
            _queueType = queueType;
            _leagueService = leagueService;
            _webApiService = webApiService;
        }

        protected override async Task DoWorkAsync(CancellationToken cancellationToken)
        {
            League league;
            var regionString = _region.ToString();

            if (_tierLeague == TierLeague.Challenger)
            {
                league = await _leagueService.GetChallengerTierLeaguesAsync(_region, _queueType);
            }
            else
            {
                league = await _leagueService.GetMasterTierLeaguesAsync(_region, _queueType);
            }

            Logger.Debug($"{GetParameterString()}: Found {league.Entries.Count} League Entries.");

            // apply region
            foreach (var leagueEntry in league.Entries)
            {
                leagueEntry.Region = regionString;
            }

            await _webApiService.PostLeagueAsync(league);
        }

        public override string ToString()
        {
            return $"{GetType().Name}{GetParameterString()}>";
        }

        private string GetParameterString()
        {
            return $"<{_region}, {_tierLeague}, {_queueType}>";
        }
    }
}
