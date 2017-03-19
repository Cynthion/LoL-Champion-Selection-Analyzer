using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Models.Dtos.Summoner;

namespace WebApi.Services.Interfaces
{
    public interface ISummonerService
    {
        Task<IEnumerable<Pair<SummonerDto>>> GetSummonersByNameAsync(IEnumerable<string> summonerNames);
    }
}
