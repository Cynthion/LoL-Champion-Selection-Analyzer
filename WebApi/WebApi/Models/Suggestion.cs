using System.Collections.Generic;

namespace WebApi.Models
{
    public class Suggestion
    {
        public IEnumerable<long> TopSuggestions { get; set; }

        public IEnumerable<long> JglSuggestions { get; set; }

        public IEnumerable<long> MidSuggestions { get; set; }

        public IEnumerable<long> BotSuggestions { get; set; }

        public IEnumerable<long> SupSuggestions { get; set; }
    }
}
