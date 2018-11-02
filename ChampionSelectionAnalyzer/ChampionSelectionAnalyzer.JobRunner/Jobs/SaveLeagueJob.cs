using System;
using System.Threading;
using System.Threading.Tasks;
using ChampionSelectionAnalyzer.JobRunner.Framework;
using ChampionSelectionAnalyzer.RiotModel.League;

namespace ChampionSelectionAnalyzer.JobRunner.Jobs
{
    internal class SaveLeagueJob : JobBase<string>
    {
        private readonly LeagueListDto _leagueListDto;

        internal SaveLeagueJob(LeagueListDto leagueListDto)
        {
            _leagueListDto = leagueListDto;
        }

        protected override Task<string> DoWorkAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
