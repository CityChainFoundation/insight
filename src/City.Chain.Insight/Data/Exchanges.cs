using City.Chain.Insight.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace City.Chain.Insight.Data
{
    public interface IExchange
    {
        Task Add(Exchange exchange);

        Task Remove(string identifier);

        List<Exchange> GetExchanges(string draw);

        Task<Exchange> Get(string identifier);
    }

    public class Exchanges : IExchange
    {
        private DatabaseContext db;

        public Exchanges(DatabaseContext context)
        {
            this.db = context;
        }

        public List<Exchange> GetExchanges(string draw) => db.Exchanges.Find(x => true).ToList().OrderByDescending(x => x.Title).ToList();

        public async Task Add(Exchange exchange)
        {
            ReplaceOneResult replaceOneResult = await db.Exchanges.ReplaceOneAsync(Builders<Exchange>.Filter.Eq(r => r.Identifier, exchange.Identifier), exchange, new UpdateOptions() { IsUpsert = true });

            Console.WriteLine(replaceOneResult.IsAcknowledged);
            Console.WriteLine(replaceOneResult.MatchedCount);
            Console.WriteLine(replaceOneResult.ModifiedCount);
            Console.WriteLine(replaceOneResult.UpsertedId);

            //var filter = new BsonDocument("FirstName", "Jack");
            //var update = Builders<BsonDocument>.Update.Set("FirstName", "John");

            //await db.Exchanges.FindOneAndUpdateAsync(Builders<BsonDocument>.Filter.Eq("identifier", exchange.Identifier), exchange);

            //var result = await db.Exchanges.FindOneAndUpdateAsync(
            //          Builders<BsonDocument>.Filter.Eq("identifier", exchange.Identifier),
            //          Builders<BsonDocument>.Update.Set("MasterID", 1120)
            //          );

            //var result = await db.Exchanges.FindOneAndUpdateAsync(e => e., update);

            //var filter = new BsonDocument("FirstName", "Jack");
            //var replacementDocument = new BsonDocument("FirstName", "John");

            //var result = await collection.FindOneAndReplaceAsync(filter, doc);

            //if (result != null)
            //{
            //    Assert(result["FirstName"] == "Jack");
            //}


            //await db.Exchanges.FindOneAndUpdateAsync()
            //await db.Exchanges.InsertOneAsync(exchange);
        }

        public async Task Remove(string identifier)
        {
            await db.Exchanges.DeleteOneAsync(e => e.Identifier == identifier);
        }

        public async Task<Exchange> Get(string identifier)
        {
            var query = await db.Exchanges.FindAsync(e => e.Identifier == identifier);
            return query.FirstOrDefault();
        }

        //public string GetLastDraw()
        //{
        //    return db.Exchanges.Find(x => true).ToList().OrderByDescending(x => x.Title).FirstOrDefault().Id.ToString();
        //}

        //public void StoreParticipation(string ticket, string nickname, string address, double amount)
        //{
        //    _databaseContext.Participations.InsertOne(new Participation
        //    {
        //        CreationDate = DateTime.Now,
        //        Ticket = ticket,
        //        Nickname = nickname,
        //        WithdrawAddress = address,
        //        Draw = _draws.GetLastDraw(),
        //        Amount = amount
        //    });
        //}
    }
}
