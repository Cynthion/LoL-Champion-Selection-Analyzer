using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Model.Dtos.League;

namespace WebApi.RiotJobRunner
{
    internal interface IWebApiService
    {
        Task<IEnumerable<LeagueEntry>> GetLeaguesAsync();

        Task SendLeagueAsync(League league);
    }
}
