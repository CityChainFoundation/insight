//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;

//// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace City.Chain.Insight.Controllers
//{
//    [Route("api/tx")]
//    [ApiController]
//    public class TransactionController : Controller
//    {
//        // GET: api/<controller>
//        [HttpGet]
//        public IEnumerable<string> Get()
//        {
//            return new string[] { "value1", "value2" };
//        }

//        /// <summary>
//        /// Returns the transaction in a formatted JSON output. Use the /hex to get raw transaction data.
//        /// </summary>
//        /// <param name="txid"></param>
//        /// <returns></returns>
//        [HttpGet("{id}")]
//        public string Get(string txid)
//        {
//            return "value";
//        }

//        /// <summary>
//        /// Use this to get the raw transaction data.
//        /// </summary>
//        /// <param name="txid"></param>
//        /// <returns>Hex of the transaction.</returns>
//        [HttpGet("{id}/hex")]
//        public string GetRaw(string txid)
//        {
//            return "12312342342";
//        }

//        [HttpPost]
//        public void Post([FromBody]string value)
//        {
//        }

//        // PUT api/<controller>/5
//        [HttpPut("{id}")]
//        public void Put(int id, [FromBody]string value)
//        {
//        }

//        // DELETE api/<controller>/5
//        [HttpDelete("{id}")]
//        public void Delete(int id)
//        {
//        }
//    }
//}
