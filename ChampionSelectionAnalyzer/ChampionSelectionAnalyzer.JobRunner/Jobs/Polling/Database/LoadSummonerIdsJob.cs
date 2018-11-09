using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ChampionSelectionAnalyzer.JobRunner.Framework;
using ChampionSelectionAnalyzer.RiotApiClient.Misc;
using ChampionSelectionAnalyzer.RiotModel.League;
using NLog;

namespace ChampionSelectionAnalyzer.JobRunner.Jobs.Polling.Database
{
    internal class LoadSummonerIdsJob : JobBase<IEnumerable<string>>
    {
        private static readonly ILogger Logger = LogManager.GetLogger(nameof(LoadSummonerIdsJob));
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
                var idPrefix = $"{nameof(LeagueListDto)}/{_region}";
                var leagueListDtos = await asyncSession.Advanced.LoadStartingWithAsync<LeagueListDto>(idPrefix, token: cancellationToken);

                var summonerIds = leagueListDtos
                                    .SelectMany(l => l.Entries.Select(e => e.PlayerOrTeamId))
                                    .Distinct();

                return summonerIds.Take(100);
            }
        }

        protected override void OnWorkCompleted(IEnumerable<string> result)
        {
            Logger.Log(LogLevel.Info, $"Loaded {result.Count()} summoner IDs.");
        }
    }
}