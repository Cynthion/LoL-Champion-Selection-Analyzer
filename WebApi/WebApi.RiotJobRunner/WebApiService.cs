using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebApi.Model.Dtos.League;
using WebApi.Model.Dtos.Match;

namespace WebApi.RiotJobRunner
{
    internal class WebApiService : IWebApiService
    {
        private const string Domain = "http://localhost:5000";

        private readonly HttpClient _httpClient;

        public WebApiService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<long> GetLeagueEntryCountAsync()
        {
            var url = $"{Domain}/api/leagueentry/count";

            var response = await GetRequestAsync(url);

            return long.Parse(response);
        }

        public async Task<long> GetMatchReferenceCountAsync()
        {
            var url = $"{Domain}/api/matchreference/count";

            var response = await GetRequestAsync(url);

            return long.Parse(response);
        }

        public async Task<IEnumerable<LeagueEntry>> GetLeagueEntriesAsync()
        {
            var url = $"{Domain}/api/leagueentry";

            var response = await GetRequestAsync(url);

            return JsonConvert.DeserializeObject<LeagueEntry[]>(response);
        }

        public async Task<IEnumerable<MatchReference>> GetMatchReferencesAsync()
        {
            var url = $"{Domain}/api/matchreference";

            var response = await GetRequestAsync(url);

            return JsonConvert.DeserializeObject<MatchReference[]>(response);
        }

        public async Task PostLeagueAsync(League league)
        {
            var url = $"{Domain}/api/leagueentry";

            // TODO batch
            foreach (var leagueEntry in league.Entries)
            {
                var jsonObject = JsonConvert.SerializeObject(leagueEntry);

                await PostRequestAsync(url, jsonObject);
            }
        }

        public async Task PostMatchlistAsync(MatchList matchlist)
        {
            var url = $"{Domain}/api/matchreference";

            // TODO batch
            foreach (var matchReference in matchlist.Matches)
            {
                var jsonObject = JsonConvert.SerializeObject(matchReference);

                await PostRequestAsync(url, jsonObject);
            }
        }

        private async Task<string> GetRequestAsync(string url)
        {
            string result;

            using (var response = await _httpClient.GetAsync(url))
            {
                response.EnsureSuccessStatusCode();

                using (var content = response.Content)
                {
                    result = await content.ReadAsStringAsync();
                }
            }

            return result;
        }

        private async Task PostRequestAsync(string url, string content)
        {
            using (var response = await _httpClient.PostAsync(url, new StringContent(content, Encoding.UTF8, "application/json")))
            {
                response.EnsureSuccessStatusCode();
            }
        }
    }
}
