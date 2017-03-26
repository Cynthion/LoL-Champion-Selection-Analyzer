using System;
using System.IO;
using Newtonsoft.Json;
using WebApi.Misc.Interfaces;

namespace WebApi.Misc
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

                Console.WriteLine($"Using Api Key:\n{ content }");

                var riotApiKey = JsonConvert.DeserializeObject<RiotApiKey>(content);

                return riotApiKey;
            }
        }

    }
}
