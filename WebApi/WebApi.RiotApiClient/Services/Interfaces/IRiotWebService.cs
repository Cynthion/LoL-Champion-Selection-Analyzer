using System.Threading.Tasks;
using WebApi.Model.Enums;

namespace WebApi.RiotApiClient.Services.Interfaces
{
    public interface IRiotWebService
    {
        Task<string> GetRequestAsync(Region region, string url);
    }
}
