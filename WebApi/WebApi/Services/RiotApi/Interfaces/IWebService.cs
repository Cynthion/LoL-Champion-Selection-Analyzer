using System.Threading.Tasks;

namespace WebApi.Services.RiotApi.Interfaces
{
    public interface IWebService
    {
        Task<string> GetRequestAsync(string url);
    }
}
