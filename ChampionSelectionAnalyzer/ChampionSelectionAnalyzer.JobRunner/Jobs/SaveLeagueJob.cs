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

        protected override async Task<string> DoWorkAsync(CancellationToken cancellationToken)
        {
            using (var session = RavenDb.Store.OpenAsyncSession())
            {
                await session.StoreAsync(_leagueListDto, _leagueListDto.LeagueId, cancellationToken);

                await session.SaveChangesAsync(cancellationToken);
            }

            return string.Empty;
        }
    }
}
