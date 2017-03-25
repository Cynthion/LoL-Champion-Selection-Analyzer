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
        private IDictionary<long, IDictionary<Lane, IDictionary<Lane, IEnumerable<Matchup>>>> _matchups;

        public SuggestionService()
        {
            // TODO manage this in an external MatchupService
            // TODO ensure matchups are correct -> champion on lane is entered correctly
            _matchups = new ConcurrentDictionary<long, IDictionary<Lane, IDictionary<Lane, IEnumerable<Matchup>>>>();
        }

        /// <summary>
        /// Get a suggestion for the <see cref="friendlyTeam"/> team if it plays against the <see cref="enemyTeam"/> team.
        /// </summary>
        /// <param name="friendlyTeam"></param>
        /// <param name="enemyTeam"></param>
        /// <param name="championBans"></param>
        /// <returns></returns>
        public Suggestion GetSuggestion(TeamConstellation friendlyTeam, TeamConstellation enemyTeam, IEnumerable<long> championBans)
        {
            // TODO parallelize
            var suggestion = new Suggestion();

            var friendlyPicks = friendlyTeam.ChampionPlacements.Where(p => p.IsPicked).ToArray();
            var enemyPicks = enemyTeam.ChampionPlacements.Where(p => p.IsPicked).ToArray();

            // TODO filter out bannedOrPicked
            var bannedOrPicked = friendlyPicks.Select(p => p.ChampionId)
                .Concat(enemyPicks.Select(p => p.ChampionId))
                .Concat(championBans)
                .Distinct();

            // TODO give suggestions only for placements where not already set
            foreach (var placementUnderSuggestion in friendlyTeam.ChampionPlacements)
            {
                // the lane we are looking for suggestions for
                var laneUnderSuggestion = placementUnderSuggestion.Lane; // e.g., MID

                /** e.g., 
                * Maokai - TOP with Ahri - MID
                * Maokai - TOP with Veigar - MID
                * Rengar - JGL with Ahri - MID
                * Rengar - JGL with Brand - MID
                */
                var allFriendsMatchupsWithLaneUnderSuggestion = new List<Matchup>();

                /**
                * e.g.,
                * Maokai - TOP
                * Rengar - JGL
                * no BOT               - not announced
                * <Soraka> - SUP       - announced
                */
                foreach (var friendlyPick in friendlyPicks)
                {
                    var friendlyChampion = friendlyPick.ChampionId; // e.g., Maokai
                    var friendlyLane = friendlyPick.Lane; // e.g., Top
                    var friendsMatchupsForHisLane = _matchups[friendlyChampion][friendlyLane]; // TODO join these keys?

                    // filter by laneUnderSuggestion
                    // -> matchups with friend for laneUnderSuggestion
                    /** e.g., 
                    * Maokai - TOP with Ahri - MID
                    * Maokai - TOP with Veigar - MID
                    */
                    var friendsMatchupsWithLaneUnderSuggestion = friendsMatchupsForHisLane[laneUnderSuggestion];

                    allFriendsMatchupsWithLaneUnderSuggestion.AddRange(friendsMatchupsWithLaneUnderSuggestion);
                }

                /** e.g., 
                * Garen - TOP with Ahri - MID
                * Garen - TOP with Karma - MID
                * Yi - JGL with Ahri - MID
                * Yi - JGL with Syndra - MID
                */
                var allEnemiesMatchupsWithLaneUnderSuggestion = new List<Matchup>();

                /**
                 * e.g.,
                 * Garen - TOP
                 * Yi - JGL
                 */
                foreach (var enemyPick in enemyPicks)
                {
                    var enemyChampion = enemyPick.ChampionId; // e.g., Garen
                    var enemyLane = enemyPick.Lane; // e.g., Top
                    var enemysMatchupsForHisLane = _matchups[enemyChampion][enemyLane];

                    // filter by laneUnderSuggestion
                    // -> matchups with enemy for laneUnderSuggestion
                    /** e.g., 
                    * Garen - TOP with Ahri - MID
                    * Garen - TOP with Karma - MID
                    */
                    var enemysMatchupsWithLaneUnderSuggestion = enemysMatchupsForHisLane[laneUnderSuggestion];

                    allEnemiesMatchupsWithLaneUnderSuggestion.AddRange(enemysMatchupsWithLaneUnderSuggestion);
                }
                
                var bestChampions = FindBestChampions(
                    allFriendsMatchupsWithLaneUnderSuggestion,
                    allEnemiesMatchupsWithLaneUnderSuggestion);

                // TODO refactor, make clean
                switch (laneUnderSuggestion)
                {
                    case Lane.Top:
                        suggestion.TopSuggestions = bestChampions;
                        break;
                    case Lane.Jgl:
                        suggestion.JglSuggestions = bestChampions;
                        break;
                    case Lane.Mid:
                        suggestion.MidSuggestions = bestChampions;
                        break;
                    case Lane.Bot:
                        suggestion.BotSuggestions = bestChampions;
                        break;
                    case Lane.Sup:
                        suggestion.SupSuggestions = bestChampions;
                        break;
                    default:
                        throw new NotSupportedException();
                }
            }

            return suggestion;
        }

        private static IEnumerable<ChampionPotential> FindBestChampions(IList<Matchup> allFriendsMatchupsWithLaneUnderSuggestion, IList<Matchup> allEnemiesMatchupsWithLaneUnderSuggestion)
        {
            // assume all matchups exist -> TODO create all possible matchups
            var champions = allFriendsMatchupsWithLaneUnderSuggestion
                .Concat(allEnemiesMatchupsWithLaneUnderSuggestion)
                .Select(m => m.ChampionPlacement2)
                .Distinct();

            var championPotentials = new List<ChampionPotential>();

            foreach (var championId in champions.Select(c => c.ChampionId))
            {
                var championPotential = new ChampionPotential(championId)
                {
                    AvgSynergyWinRate = allFriendsMatchupsWithLaneUnderSuggestion
                        .Where(m => championId == m.ChampionPlacement2.ChampionId)
                        .Average(m => m.SynergyWinRate),
                    AvgCounterWinRate = allEnemiesMatchupsWithLaneUnderSuggestion
                        .Where(m => championId == m.ChampionPlacement2.ChampionId)
                        .Average(m => m.CounterWinRate)
                };

                championPotentials.Add(championPotential);
            }

            return championPotentials.OrderByDescending(p => p.Potential).Take(3);
        }
    }
}
