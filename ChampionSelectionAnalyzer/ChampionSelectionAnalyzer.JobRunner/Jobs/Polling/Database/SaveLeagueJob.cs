using System.Threading;
using System.Threading.Tasks;
using ChampionSelectionAnalyzer.JobRunner.Framework;
using ChampionSelectionAnalyzer.RiotModel.League;
using NLog;

namespace ChampionSelectionAnalyzer.JobRunner.Jobs.Polling.Database
{
    internal class SaveLeagueJob : JobBase<LeagueListDto>
    {
        private static readonly ILogger Logger = LogManager.GetLogger(nameof(SaveLeagueJob));

        private readonly LeagueListDto _leagueListDto;

        internal SaveLeagueJob(LeagueListDto leagueListDto)
        {
            _leagueListDto = leagueListDto;
        }

        protected override async Task<LeagueListDto> DoWorkAsync(CancellationToken cancellationToken)
        {
            using (var asyncSession = RavenDb.Store.OpenAsyncSession())
            {
                await asyncSession.StoreAsync(_leagueListDto, _leagueListDto.Id(), cancellationToken);

                await asyncSession.SaveChangesAsync(cancellationToken);

                return _leagueListDto;
            }
        }

        protected override void OnWorkCompleted(LeagueListDto leagueListDto)
        {
            Logger.Log(LogLevel.Info, $"Saved { leagueListDto.Name() } to database.");
        }
    }
}
