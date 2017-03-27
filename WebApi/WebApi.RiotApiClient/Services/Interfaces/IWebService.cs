using System.Threading.Tasks;
using WebApi.RiotApiClient.Misc;

namespace WebApi.RiotApiClient.Services.Interfaces
{
    public interface IWebService
    {
        Task<string> GetRequestAsync(Region region, string url);
    }
}
