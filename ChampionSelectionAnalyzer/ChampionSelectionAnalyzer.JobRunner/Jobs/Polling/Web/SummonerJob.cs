using System;
using System.Threading;
using System.Threading.Tasks;
using ChampionSelectionAnalyzer.JobRunner.Framework;
using ChampionSelectionAnalyzer.RiotApiClient.Misc;
using ChampionSelectionAnalyzer.RiotApiClient.Services.Interfaces;
using ChampionSelectionAnalyzer.RiotModel.Summoner;

namespace ChampionSelectionAnalyzer.JobRunner.Jobs.Polling.Web
{
    internal class SummonerJob : JobBase<SummonerDto>
    {
        private readonly Region _region;
        private readonly long _summonerId;
        private readonly ISummonerService _summonerService;

        internal SummonerJob(
            Region region,
            long summonerId,
            ISummonerService summonerService,
            Action<SummonerDto> resultAction)
            : base(resultAction)
        {
            _region = region;
            _summonerId = summonerId;
            _summonerService = summonerService;
        }

        protected override Task<SummonerDto> DoWorkAsync(CancellationToken cancellationToken)
        {
            return _summonerService.GetSummonerByIdAsync(_region, _summonerId);
        }

        public override string ToString()
        {
            return $"{GetType().Name}<{_region}, {_summonerId}>";
        }
    }
}
