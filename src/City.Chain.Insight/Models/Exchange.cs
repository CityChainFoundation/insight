using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace City.Chain.Insight.Models
{
    public class Exchange
    {
        //[BsonId]
        //public ObjectId Id { get; set; }

        [BsonElement("created")]
        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [BsonElement("updated")]
        [JsonProperty("updated")]
        public DateTime Updated { get; set; }

        [BsonElement("identifier")]
        [JsonProperty("identifier")]
        public string Identifier { get; set; }

        [BsonElement("title")]
        [JsonProperty("title")]
        public string Title { get; set; }

        [BsonElement("url")]
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
