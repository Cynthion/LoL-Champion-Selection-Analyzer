using System.Threading.Tasks;
using WebApi.Misc;

namespace WebApi.Services.RiotApi.Interfaces
{
    public interface IWebService
    {
        Task<string> GetRequestAsync(Region region, string url);
    }
}
