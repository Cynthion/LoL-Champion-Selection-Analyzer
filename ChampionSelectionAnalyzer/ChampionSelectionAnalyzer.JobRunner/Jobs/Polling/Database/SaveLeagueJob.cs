using System.Threading;
using System.Threading.Tasks;
using ChampionSelectionAnalyzer.JobRunner.Framework;
using ChampionSelectionAnalyzer.RiotModel.League;

namespace ChampionSelectionAnalyzer.JobRunner.Jobs.Polling.Database
{
    internal class SaveLeagueJob : JobBase
    {
        private readonly LeagueListDto _leagueListDto;

        internal SaveLeagueJob(LeagueListDto leagueListDto)
        {
            _leagueListDto = leagueListDto;
        }

        protected override async Task DoWorkAsync(CancellationToken cancellationToken)
        {
            using (var asyncSession = RavenDb.Store.OpenAsyncSession())
            {
                await asyncSession.StoreAsync(_leagueListDto, _leagueListDto.Id(), cancellationToken);

                await asyncSession.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
