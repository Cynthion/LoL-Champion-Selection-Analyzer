using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.RiotApiClient.Dtos.Summoner;
using WebApi.RiotApiClient.Misc;

namespace WebApi.RiotApiClient.Services.Interfaces
{
    public interface ISummonerService
    {
        Task<IDictionary<string, SummonerDto>> GetSummonersByNameAsync(Region region, IEnumerable<string> summonerNames);
    }
}
