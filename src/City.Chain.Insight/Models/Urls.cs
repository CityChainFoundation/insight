using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace City.Chain.Insight.Models
{
    public class Urls
    {
        public Urls()
        {
            Website = new List<string>();
            Explorer = new List<string>();
            Source = new List<string>();
            Board = new List<string>();
            Chat = new List<string>();
            Announcement = new List<string>();
            Reddit = new List<string>();
            Twitter = new List<string>();
        }

        [JsonProperty("website")]
        public List<string> Website { get; set; }

        [JsonProperty("explorer")]
        public List<string> Explorer { get; set; }

        [JsonProperty("source")]
        public List<string> Source { get; set; }

        [JsonProperty("board")]
        public List<string> Board { get; set; }

        [JsonProperty("chat")]
        public List<string> Chat { get; set; }

        [JsonProperty("announcement")]
        public List<string> Announcement { get; set; }

        [JsonProperty("reddit")]
        public List<string> Reddit { get; set; }

        [JsonProperty("twitter")]
        public List<string> Twitter { get; set; }
    }
}
