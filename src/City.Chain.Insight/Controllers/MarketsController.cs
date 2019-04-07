using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace City.Chain.Insight.Controllers
{
	[Route("api/market")]
	public class MarketsController : Controller
	{
		[HttpGet("{id}")]
		[ResponseCache(Duration = 30)]
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

			var client = new RestClient("https://p2pb2b.io/");
			var request = new RestRequest("api/v1/public/ticker?market={market}", Method.GET);
			request.RequestFormat = DataFormat.Json;
			request.AddParameter("market", market, ParameterType.QueryString);

			IRestResponse response = client.Execute(request);
			var content = response.Content;
			return content;
		}
	}
}
