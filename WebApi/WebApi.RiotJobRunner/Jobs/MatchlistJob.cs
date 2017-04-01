using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NLog;
using WebApi.RiotApiClient.Misc;
using WebApi.RiotApiClient.Services.Interfaces;

namespace WebApi.RiotJobRunner.Jobs
{
    //internal class MatchListJob : JobBase<IEnumerable<long>>
    //{
    //    private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

    //    private readonly Region _region;
    //    private readonly long _summonerId;
    //    private readonly IEnumerable<string> _rankedQueues;
    //    private readonly IEnumerable<string> _seasons;
    //    private readonly IMatchService _matchService;

    //    public MatchListJob(
    //        Region region,
    //        long summonerId,
    //        IEnumerable<string> rankedQueues,
    //        IEnumerable<string> seasons,
    //        IMatchService matchService,
    //        Action<IEnumerable<long>> resultAction)
    //        : base(resultAction)
    //    {
    //        _region = region;
    //        _summonerId = summonerId;
    //        _rankedQueues = rankedQueues;
    //        _seasons = seasons;
    //        _matchService = matchService;
    //    }

    //    protected override async Task<IEnumerable<long>> DoWorkAsync(CancellationToken cancellationToken)
    //    {
    //        var matchListDto = await _matchService.GetMatchListAsync(_region, _summonerId, _rankedQueues.ToArray(), _seasons.ToArray());

    //        var matchIds = new List<long>();
    //        if (matchListDto?.Matches != null && matchListDto.Matches.Any())
    //        {
    //            var ids = matchListDto.Matches
    //                .Select(e => e.MatchId)
    //                .Distinct();

    //            matchIds.AddRange(ids);
    //        }

    //        Logger.Debug($"{GetParameterString()}: Found {matchIds.Count} Match IDs.");

    //        return matchIds;
    //    }

    //    public override string ToString()
    //    {
    //        return $"{GetType().Name}{GetParameterString()}>";
    //    }

    //    private string GetParameterString()
    //    {
    //        return $"<{_region}, Summoner ID: {_summonerId}, {string.Join("/", _rankedQueues)}, {string.Join("/", _seasons)}>";
    //    }
    //}
}
