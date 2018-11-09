using System.Threading;
using System.Threading.Tasks;
using ChampionSelectionAnalyzer.JobRunner.Framework;
using ChampionSelectionAnalyzer.RiotModel.Summoner;
using NLog;

namespace ChampionSelectionAnalyzer.JobRunner.Jobs.Polling.Database
{
    internal class SaveSummonerJob : JobBase<SummonerDto>
    {
        private static readonly ILogger Logger = LogManager.GetLogger(nameof(SaveSummonerJob));

        private readonly SummonerDto _summonerDto;

        internal SaveSummonerJob(SummonerDto summonerDto)
        {
            _summonerDto = summonerDto;
        }

        protected override async Task<SummonerDto> DoWorkAsync(CancellationToken cancellationToken)
        {
            using (var asyncSession = RavenDb.Store.OpenAsyncSession())
            {
                await asyncSession.StoreAsync(_summonerDto, _summonerDto.Id(), cancellationToken);

                await asyncSession.SaveChangesAsync(cancellationToken);

                return _summonerDto;
            }
        }

        protected override void OnWorkCompleted(SummonerDto summonerDto)
        {
            Logger.Log(LogLevel.Info, $"Saved { summonerDto.Name() } to database.");
        }
    }
}
