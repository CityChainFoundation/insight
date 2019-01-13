using City.Chain.Insight.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace City.Chain.Insight
{
    public class DatabaseContext
    {
        private readonly IMongoDatabase _database;

        public DatabaseContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("MongoDB"));

            if (client != null)
            {
                _database = client.GetDatabase("city-chain-insight");
            }
        }

        public IMongoCollection<Exchange> Exchanges => _database.GetCollection<Exchange>("exchanges");
    }
}
