using System.IO;
using ChampionSelectionAnalyzer.RiotApiClient.Misc.Interfaces;
using Newtonsoft.Json;

namespace ChampionSelectionAnalyzer.RiotApiClient.Misc
{
    public class RiotApiKey : IApiKey
    {
        public bool IsProduction { get; set; }

        public string ApiKey { get; set; }

        private RiotApiKey()
        {

        }

        public static RiotApiKey CreateFromFile()
        {
            using (var reader = new StreamReader(File.Open("riot-api-key.json", FileMode.Open)))
            {
                var content = reader.ReadToEnd();

                var riotApiKey = JsonConvert.DeserializeObject<RiotApiKey>(content);

                return riotApiKey;
            }
        }

        public override string ToString()
        {
            return $"{ ApiKey }, IsProduction: { IsProduction }";
        }
    }
}
