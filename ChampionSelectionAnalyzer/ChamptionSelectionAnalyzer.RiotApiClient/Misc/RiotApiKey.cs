using System.IO;
using ChampionSelectionAnalyzer.RiotApiClient.Misc.Interfaces;
using Newtonsoft.Json;
using NLog;

namespace ChampionSelectionAnalyzer.RiotApiClient.Misc
{
    public class RiotApiKey : IApiKey
    {
        private static readonly ILogger Logger = LogManager.GetLogger(nameof(RiotApiKey));

        public bool IsProduction { get; set; }

        public string ApiKey { get; set; }

        private RiotApiKey()
        { }

        public static RiotApiKey CreateFromFile()
        {
            using (var reader = new StreamReader(File.Open("riot-api-key.json", FileMode.Open)))
            {
                var content = reader.ReadToEnd();

                var riotApiKey = JsonConvert.DeserializeObject<RiotApiKey>(content);
                Logger.Log(LogLevel.Info, $"Created { riotApiKey }.");

                return riotApiKey;
            }
        }

        public override string ToString()
        {
            return $"<{ ApiKey }, production: { IsProduction.ToString().ToLower() }>";
        }
    }
}
