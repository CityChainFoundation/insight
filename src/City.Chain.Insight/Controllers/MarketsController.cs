using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RestSharp;

namespace City.Chain.Insight.Controllers
{
    [Route("api/market")]
    public class MarketsController : Controller
    {
        private readonly IMemoryCache cache;

        public MarketsController(IMemoryCache memoryCache)
        {
            this.cache = memoryCache;
        }

        [HttpGet("{id}")]
        [ResponseCache(VaryByQueryKeys = new string[] { "*" }, Duration = 300)]
        public string Get(string id)
        {
            string market;

            if (id.ToLower() == "usd")
            {
                market = "CITY_USD";
            }
            else
            {
                market = "CITY_BTC";
            }

            string cacheKey = $"{CacheKeys.MarketDetails}{market}";
            string marketContent;

            if (!cache.TryGetValue(cacheKey, out marketContent))
            {
                var client = new RestClient("https://p2pb2b.io/");
                var request = new RestRequest("api/v1/public/ticker?market={market}", Method.GET);
                request.RequestFormat = DataFormat.Json;
                request.AddParameter("market", market, ParameterType.QueryString);

                IRestResponse response = client.Execute(request);
                var content = response.Content;
                marketContent = content;

                var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

                // Save data in cache.
                cache.Set(cacheKey, marketContent, cacheEntryOptions);
            }

            return marketContent;
        }
    }
}
