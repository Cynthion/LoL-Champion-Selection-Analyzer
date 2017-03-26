using System.Threading.Tasks;
using WebApi.Misc;
using WebApi.Models.Dtos.League;

namespace WebApi.Services.RiotApi.Interfaces
{
    public interface ILeagueService
    {
        Task<LeagueDto> GetMasterTierLeagues(Region region, string queueType);

        Task<LeagueDto> GetChallengerTierLeagues(Region region, string queueType);
    }
}
