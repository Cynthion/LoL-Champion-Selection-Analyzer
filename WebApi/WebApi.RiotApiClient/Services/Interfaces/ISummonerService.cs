using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Model.Enums;
using WebApi.Model.RiotDtos.Summoner;

namespace WebApi.RiotApiClient.Services.Interfaces
{
    public interface ISummonerService
    {
        Task<IDictionary<string, SummonerDto>> GetSummonersByNameAsync(Region region, IEnumerable<string> summonerNames);
    }
}
