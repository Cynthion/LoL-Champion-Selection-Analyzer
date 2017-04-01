using System.Text;
using System.Threading.Tasks;
using WebApi.Model.Dtos.League;

namespace WebApi.RiotJobRunner
{
    internal interface IWebApiService
    {
        Task SendLeagueDtoAsync(League league);
    }
}
