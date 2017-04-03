using System.Threading.Tasks;
using WebApi.Model.Enums;
using WebApi.Model.RiotDtos.League;
using WebApi.RiotApiClient.Misc;

namespace WebApi.RiotApiClient.Services.Interfaces
{
    public interface ILeagueService
    {
        Task<LeagueDto> GetMasterTierLeaguesAsync(Region region, string queueType);

        Task<LeagueDto> GetChallengerTierLeaguesAsync(Region region, string queueType);
    }
}
