using City.Chain.Insight.Middleware;
using City.Chain.Insight.Models;
using City.Chain.Insight.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace City.Chain.Insight.Controllers
{
    [Route("api/economy")]
    public class EconomyController : Controller
    {
        private readonly IMemoryCache cache;
        private readonly BlockIndexService blockService;

        public EconomyController(IMemoryCache memoryCache, BlockIndexService blockService)
        {
            this.cache = memoryCache;
            this.blockService = blockService;
        }

        // GET: api/<controller>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<controller>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        [HttpGet("currency")]
        public async Task<ActionResult<ApiResponse<CurrencyDetails>>> GetCurrencyDetails()
        {
            try
            {
                CurrencyDetails currencyDetails;

                if (!cache.TryGetValue(CacheKeys.CurrencyDetails, out currencyDetails))
                {
                    // TODO: Funds should be stored in MongoDB, and be editable by individual chains and not hard-coded.
                    currencyDetails = new CurrencyDetails();

                    currencyDetails.Name = "City Coin";
                    currencyDetails.Symbol = "CITY";
                    currencyDetails.Logo = "https://city-chain.org/images/icons/city-coin-128x128.png";

                    currencyDetails.Urls = new Urls();
                    currencyDetails.Urls.Website.Add("https://city-chain.org/");
                    currencyDetails.Urls.Website.Add("https://citychain.foundation/");
                    currencyDetails.Urls.Explorer.Add("https://explorer.city-chain.org/");
                    currencyDetails.Urls.Source.Add("https://github.com/CityChainFoundation/");
                    currencyDetails.Urls.Board.Add("https://bitcointalk.org/index.php?topic=5073402.0");
                    currencyDetails.Urls.Board.Add("https://cryptocurrencytalk.com/topic/114297-anncity-city-chain-blockchain-for-the-smart-city-platform/");
                    currencyDetails.Urls.Chat.Add("https://t.me/citychain");
                    currencyDetails.Urls.Chat.Add("https://discord.gg/CD8CTJt");
                    currencyDetails.Urls.Reddit.Add("https://www.reddit.com/user/citychain");
                    currencyDetails.Urls.Twitter.Add("https://twitter.com/citychaincoin");

                    double premine = 13736000000;

                    var funds = RetrieveFunds();

                    double walletsBalance = 0;
                    double originalBalance = 0;

                    foreach (var fund in funds)
                    {
                        var address = this.blockService.GetTransactionsByAddress(fund.Address);
                        currencyDetails.Wallets.Add(address);

                        walletsBalance += address.Balance;
                        originalBalance += fund.InitialAmount;
                    }

                    var latestBlock = this.blockService.GetLatestBlock();

                    // Even though some of the initial blocks was POW with reward output of 2, we'll calculate as if all was POS with 20 reward.
                    var rewardBalance = latestBlock.BlockIndex * 20;

                    // To calculate the approximate total supply, we do the following:
                    // PREMINE + REWARD.
                    double totalSupply = premine + rewardBalance;

                    // To calculate the approximate circulating supply, we do the following:
                    // Number of days since genesis to find the funds daily amount to spend.
                    var days = (365 * 200);
                    var spendablePrDay = originalBalance / days;
                    var genesisDate = new DateTime(2018, 10, 2, 12, 0, 0, DateTimeKind.Utc);
                    var daysSinceGenesis = (DateTime.UtcNow - genesisDate).TotalDays;

                    // Take the total amount that the funds have received + the reward balance thus far.
                    double circulatingSupply = (spendablePrDay * daysSinceGenesis) + rewardBalance;

                    // There is no value in showing decimals, so round the values before returning.
                    currencyDetails.CirculatingSupply = Math.Round(circulatingSupply, 0);
                    currencyDetails.TotalSupply = Math.Round(totalSupply, 0);

                    // var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(3));
                    var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10));

                    // Save data in cache.
                    cache.Set(CacheKeys.CurrencyDetails, currencyDetails, cacheEntryOptions);
                }

                return new ApiResponse<CurrencyDetails>(200, "Success", currencyDetails);
            }
            catch (Exception ex)
            {
                var error = new ApiError(ex.Message);
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new ApiResponse<CurrencyDetails>(500, "Error", null, error);
            }
        }

        [HttpGet("funds")]
        public async Task<ActionResult<ApiResponse<List<Fund>>>> GetFunds()
        {
            List<Fund> funds;

            if (!cache.TryGetValue(CacheKeys.Funds, out funds))
            {
                
                funds = RetrieveFunds();

                // var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(3));
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10));

                // Save data in cache.
                cache.Set(CacheKeys.Funds, funds, cacheEntryOptions);
            }

            return new ApiResponse<List<Fund>>(200, "Success", funds);
        }

        private List<Fund> RetrieveFunds()
        {
            // TODO: Funds should be stored in MongoDB, and be editable by individual chains and not hard-coded.
            var funds = new List<Fund>();

            funds.Add(new Fund
            {
                Name = "City Chain Foundation",
                Url = "https://citychain.foundation",
                Address = "CU78ydNu7odWCEtrCcCSd3fN6hzZqBUyjz",
                InitialAmount = 2747200000,
                Logo = "https://city-chain.org/images/logo/city-chain-foundation-vertical.png"
            });

            funds.Add(new Fund
            {
                Name = "Private City Fund",
                Url = "",
                Address = "Ccoquhaae7u6ASqQ5BiYueASz8EavUXrKn",
                InitialAmount = 7554800000,
                Logo = "https://city-chain.org/images/logo/private-city-fund-box.png"
            });

            funds.Add(new Fund
            {
                Name = "Private Business Incubator Program",
                Url = "",
                Address = "CSHuP5iNWrQ3AFPvK9ZnkcH8qvhPHp94kK",
                InitialAmount = 3434000000,
                Logo = "https://city-chain.org/images/logo/private-city-fund-box.png"
            });

            return funds;
        }

        // POST api/<controller>
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/<controller>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
