using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using WebApi.Misc;
using WebApi.Models;
using WebApi.Services.Interfaces;

namespace WebApi.Services
{
    public class SuggestionService : ISuggestionService
    {
        private IDictionary<Lane, IDictionary<Lane, IEnumerable<Matchup>>> _matchups;

        public SuggestionService()
        {
            // TODO manage this in an external MatchupService
            _matchups = new ConcurrentDictionary<Lane, IDictionary<Lane, IEnumerable<Matchup>>>();
        }

        /// <summary>
        /// Get a suggestion for the <see cref="friendly"/> team if it plays against the <see cref="enemy"/> team.
        /// </summary>
        /// <param name="friendly"></param>
        /// <param name="enemy"></param>
        /// <returns></returns>
        public Suggestion GetSuggestion(TeamConstellation friendly, TeamConstellation enemy, IEnumerable<long> championBans)
        {
            var friendlyPicks = friendly.GetPickedPlacements().ToArray();
            var enemyPicks = enemy.GetPickedPlacements().ToArray();
            var bannedOrPicked = friendlyPicks.Select(p => p.ChampionId)
                .Concat(enemyPicks.Select(p => p.ChampionId))
                .Concat(championBans)
                .Distinct();

            // TODO execute for all lanes
            // TODO parallelize
            // fix friendly position
            var placement = friendly.Mid;
            var midLane = placement.Lane;

            var friendlyLanes = friendlyPicks.Select(p => p.Lane).Where(l => l != midLane);
            var enemyLanes = enemyPicks.Select(p => p.Lane);

            var midLaneMatchups = _matchups[midLane];

            // counter: filter by enemy teams positions
            foreach (var enemyLane in enemyLanes)
            {
                var enemyMatchups = midLaneMatchups[enemyLane];

                // counter: algorithm over enemy champs
                var bestCounterWinRates = enemyMatchups
                    .Where(m => ChampionIsAvailable(m, bannedOrPicked))
                    .OrderByDescending(m => m.CounterWinRate)
                    .Take(3);
            }

            // synergy: filter by friendly teams positions
            foreach (var friendlyLane in friendlyLanes)
            {
                var friendlyMatchups = midLaneMatchups[friendlyLane];

                // synergy: algorithm over friednly champs
                var bestSynergyWinRates = friendlyMatchups
                    .Where(m => ChampionIsAvailable(m, bannedOrPicked))
                    .OrderByDescending(m => m.SynergyWinRate)
                    .Take(3);
            }

            return new Suggestion();
        }

        private static bool ChampionIsAvailable(Matchup matchup, IEnumerable<long> unavailableChampionIds)
        {
            return unavailableChampionIds.All(i => i != matchup.ChampionPlacement1.ChampionId);
        }
    }
}
