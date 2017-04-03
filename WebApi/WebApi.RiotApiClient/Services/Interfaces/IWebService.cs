using System.Threading.Tasks;
using WebApi.Model.Enums;

namespace WebApi.RiotApiClient.Services.Interfaces
{
    public interface IWebService
    {
        Task<string> GetRequestAsync(Region region, string url);
    }
}
