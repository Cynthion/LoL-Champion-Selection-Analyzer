using System.Threading.Tasks;

namespace WebApi.Services.Interfaces
{
    public interface IWebService
    {
        Task<string> GetRequestAsync(string url);
    }
}
