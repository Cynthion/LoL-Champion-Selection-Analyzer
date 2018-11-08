using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ChampionSelectionAnalyzer.JobRunner.Framework;
using ChampionSelectionAnalyzer.RiotApiClient.Misc;
using ChampionSelectionAnalyzer.RiotModel.League;
using Raven.Client.Documents;

namespace ChampionSelectionAnalyzer.JobRunner.Jobs.Polling.Database
{
    internal class LoadSummonerIdsJob : JobBase<IEnumerable<string>>
    {
        private readonly Region _region;

        public LoadSummonerIdsJob(Region region, Action<IEnumerable<string>> resultAction)
            : base(resultAction)
        {
            _region = region;
        }

        protected override async Task<IEnumerable<string>> DoWorkAsync(CancellationToken cancellationToken)
        {
            using (var asyncSession = RavenDb.Store.OpenAsyncSession())
            {
                var summonerIds = await asyncSession
                    .Query<LeagueListDto>()
                    .Where(l => l.Region == _region.ToString())
                    .SelectMany(d => d.Entries.Select(e => e.PlayerOrTeamId))
                    .ToListAsync(cancellationToken);

                return summonerIds;
            }
        }
    }
}