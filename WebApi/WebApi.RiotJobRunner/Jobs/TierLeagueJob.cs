using System.Threading;
using System.Threading.Tasks;
using NLog;
using WebApi.Model.Enums;
using WebApi.Model.RiotDtos.League;
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
            LeagueDto leagueDto;

            if (_tierLeague == TierLeague.Challenger)
            {
                leagueDto = await _leagueService.GetChallengerTierLeaguesAsync(_region, _queueType);
            }
            else
            {
                leagueDto = await _leagueService.GetMasterTierLeaguesAsync(_region, _queueType);
            }

            Logger.Debug($"{GetParameterString()}: Found {leagueDto.Entries.Count} LeagueDto Entries.");

            await _webApiService.PostLeagueAsync(_region, leagueDto);
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
