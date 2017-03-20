using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebApi.Models.Dtos.Summoner;
using WebApi.Services.Interfaces;
using WebApi.Misc;

namespace WebApi.Services
{
    public class SummonerService : ISummonerService
    {
        private readonly IWebService _webService;

        public SummonerService(IWebService webService)
        {
            _webService = webService;
        }

        public async Task<IDictionary<string, SummonerDto>> GetSummonersByNameAsync(Region region, IEnumerable<string> summonerNames)
        {
            var url = $"api/lol/{region}/v1.4/summoner/by-name/{string.Join(", ", summonerNames)}";

            var response = await _webService.GetRequestAsync(url);

            return JsonConvert.DeserializeObject<Dictionary<string, SummonerDto>>(response);
        }
    }
}
