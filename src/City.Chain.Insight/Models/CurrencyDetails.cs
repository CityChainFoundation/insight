using City.Chain.Insight.Models.ApiModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace City.Chain.Insight.Models
{
    public class CurrencyDetails
    {
        public CurrencyDetails()
        {
            Wallets = new List<AddressModel>();
        }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("logo")]
        public string Logo { get; set; }

        [JsonProperty("circulatingSupply")]
        public decimal CirculatingSupply { get; set; }

        [JsonProperty("totalSupply")]
        public decimal TotalSupply { get; set; }

        [JsonProperty("urls")]
        public Urls Urls { get; set; }

        [JsonProperty("wallets")]
        public List<AddressModel> Wallets { get; set; }
    }
}
