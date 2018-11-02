using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using ChampionSelectionAnalyzer.JobRunner.Framework;
using ChampionSelectionAnalyzer.RiotApiClient.Misc;
using ChampionSelectionAnalyzer.RiotApiClient.Services.Interfaces;
using ChampionSelectionAnalyzer.RiotModel.League;

namespace ChampionSelectionAnalyzer.JobRunner.Jobs
{
    internal class LeagueJob : JobBase<LeagueListDto>
    {
        private readonly Region _region;
        private readonly TierLeague _tierLeague;
        private readonly QueueType _queueType;
        private readonly ILeagueService _leagueService;

        internal LeagueJob(
            Region region,
            TierLeague tierLeague,
            QueueType queueType,
            ILeagueService leagueService)
        {
            _region = region;
            _tierLeague = tierLeague;
            _queueType = queueType;
            _leagueService = leagueService;
        }

        protected override Task<LeagueListDto> DoWorkAsync(CancellationToken cancellationToken)
        {
            switch (_tierLeague)
            {
                case TierLeague.Challenger:
                    return _leagueService.GetChallengerLeaguesByQueueAsync(_region, _queueType);
                case TierLeague.Master:
                    return _leagueService.GetMasterLeaguesByQueueAsync(_region, _queueType);
                default:
                    throw new InvalidEnumArgumentException();
            }
        }

        public override string ToString()
        {
            return $"{GetType().Name}<{_region}, {_tierLeague}, {_queueType}>";
        }
    }
}