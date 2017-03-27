using System.Threading.Tasks;
using WebApi.RiotApiClient.Dtos.League;
using WebApi.RiotApiClient.Misc;

namespace WebApi.RiotApiClient.Services.Interfaces
{
    public interface ILeagueService
    {
        Task<LeagueDto> GetMasterTierLeagues(Region region, string queueType);

        Task<LeagueDto> GetChallengerTierLeagues(Region region, string queueType);
    }
}
