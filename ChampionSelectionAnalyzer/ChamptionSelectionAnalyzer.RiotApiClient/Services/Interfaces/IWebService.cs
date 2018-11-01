using System.Threading.Tasks;
using ChampionSelectionAnalyzer.RiotApiClient.Misc;

namespace ChampionSelectionAnalyzer.RiotApiClient.Services.Interfaces
{
    public interface IWebService
    {
        Task<string> GetRequestAsync(Region region, string url);
    }
}
