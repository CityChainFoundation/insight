//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using City.Chain.Insight.Data;
//using City.Chain.Insight.Models;
//using Microsoft.AspNetCore.Mvc;

//// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace City.Chain.Insight.Controllers
//{
//    [Route("api/exchange")]
//    public class ExchangeController : Controller
//    {
//        private readonly IExchange exchanges;

//        public ExchangeController(IExchange exchanges)
//        {
//            this.exchanges = exchanges;
//        }

//        // GET: api/<controller>
//        [HttpGet]
//        public IEnumerable<string> Get()
//        {
//            return new string[] { "value1", "value2" };
//        }

//        // GET api/<controller>/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<Exchange>> Get(string id)
//        {
//            return await exchanges.Get(id);
//        }

//        // POST api/<controller>
//        [HttpPost]
//        public void Post([FromBody]string value)
//        {
//        }

//        // PUT api/<controller>/5
//        [HttpPut("{id}")]
//        public async Task Put(string id, [FromBody]Exchange data)
//        {
//            await exchanges.Add(data);
//        }

//        // DELETE api/<controller>/5
//        [HttpDelete("{id}")]
//        public async Task Delete(string id)
//        {
//            await exchanges.Remove(id);
//        }
//    }
//}
