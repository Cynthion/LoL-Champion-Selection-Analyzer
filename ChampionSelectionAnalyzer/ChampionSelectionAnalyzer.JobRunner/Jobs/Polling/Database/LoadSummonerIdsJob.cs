using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ChampionSelectionAnalyzer.JobRunner.Framework;
using ChampionSelectionAnalyzer.RiotApiClient.Misc;
using ChampionSelectionAnalyzer.RiotModel.League;

namespace ChampionSelectionAnalyzer.JobRunner.Jobs.Polling.Database
{
    internal class LoadSummonerIdsJob : JobBase<string[]>
    {
        private readonly Region _region;

        public LoadSummonerIdsJob(Region region, Action<string[]> resultAction)
            : base(resultAction)
        {
            _region = region;
        }

        protected override Task<string[]> DoWorkAsync(CancellationToken cancellationToken)
        {
            using (var session = RavenDb.Store.OpenAsyncSession())
            {
                var summonerIds = session
                    .Query<LeagueListDto>()
                    .Where(l => l.Region == _region.ToString())
                    .SelectMany(d => d.Entries.Select(e => e.PlayerOrTeamId))
                    .ToArray();

                return Task.FromResult(summonerIds);
            }
        }
    }
}
