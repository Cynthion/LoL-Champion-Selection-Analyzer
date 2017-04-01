using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NLog;
using WebApi.RiotApiClient.Misc;
using WebApi.RiotApiClient.Services.Interfaces;

namespace WebApi.RiotJobRunner.Jobs
{
    internal class MatchListJob : JobBase
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        private readonly Region _region;
        private readonly long _summonerId;
        private readonly IEnumerable<string> _rankedQueues;
        private readonly IEnumerable<string> _seasons;
        private readonly IMatchService _matchService;
        private readonly IWebApiService _webApiService;

        public MatchListJob(
            Region region,
            long summonerId,
            IEnumerable<string> rankedQueues,
            IEnumerable<string> seasons,
            IMatchService matchService,
            IWebApiService webApiService)
        {
            _region = region;
            _summonerId = summonerId;
            _rankedQueues = rankedQueues;
            _seasons = seasons;
            _matchService = matchService;
            _webApiService = webApiService;
        }

        protected override async Task DoWorkAsync(CancellationToken cancellationToken)
        {
            var matchList = await _matchService.GetMatchListAsync(_region, _summonerId, _rankedQueues.ToArray(), _seasons.ToArray());

            Logger.Debug($"{GetParameterString()}: Found {matchList.Matches.Count} Match References.");

            await _webApiService.PostMatchlistAsync(matchList);
        }

        public override string ToString()
        {
            return $"{GetType().Name}{GetParameterString()}>";
        }

        private string GetParameterString()
        {
            return $"<{_region}, Summoner ID: {_summonerId}, {string.Join("/", _rankedQueues)}, {string.Join("/", _seasons)}>";
        }
    }
}
