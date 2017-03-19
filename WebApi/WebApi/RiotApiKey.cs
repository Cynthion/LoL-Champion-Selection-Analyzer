using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebApi
{
    public class RiotApiKey
    {
        public bool IsProduction { get; private set; }

        public string ApiKey { get; private set; }

        private RiotApiKey()
        {
            
        }

        public static RiotApiKey CreateFromFile()
        {
            using (var file = File.OpenText("riot-api-key.json"))
            {
                var jsonSerializer = new JsonSerializer();
                var apiKey = (RiotApiKey)jsonSerializer.Deserialize(file, typeof(RiotApiKey));

                return apiKey;
            }
        }
    }
}
