using System.Threading;
using System.Threading.Tasks;
using ChampionSelectionAnalyzer.JobRunner.Framework;
using ChampionSelectionAnalyzer.RiotModel.Summoner;

namespace ChampionSelectionAnalyzer.JobRunner.Jobs.Polling.Database
{
    internal class SaveSummonerJob : JobBase
    {
        private readonly SummonerDto _summonerDto;

        internal SaveSummonerJob(SummonerDto summonerDto)
        {
            _summonerDto = summonerDto;
        }

        protected override async Task DoWorkAsync(CancellationToken cancellationToken)
        {
            using (var session = RavenDb.Store.OpenAsyncSession())
            {
                await session.StoreAsync(_summonerDto, _summonerDto.Id.ToString(), cancellationToken);

                await session.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
