using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Misc;
using WebApi.Models.Dtos.Summoner;

namespace WebApi.Services.Interfaces
{
    public interface ISummonerService
    {
        Task<IDictionary<string, SummonerDto>> GetSummonersByNameAsync(Region region, IEnumerable<string> summonerNames);
    }
}
