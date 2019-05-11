using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using PrBanco.Domain.Entities;
using PrBanco.Domain.ValueObjects;
using System;
using System.Threading.Tasks;

namespace PrBanco.Data.MongoDataAccess.Context
{
    public class PrBancoDbContext : MongoContext
    {

        public PrBancoDbContext(IOptions<DbSettings> settings) : base(settings)
        {

        }

        public void Seed()
        {          
            CreatePersonsTest();
            CreateIndexes();
        }

        private void CreatePersonsTest()
        {

            if (!CollectionExistsAsync("Persons").Result )
            {
                var name = new Name("André", "Castilho");
                var phone = new Phone("(41)99930-8113");
                var address = new Address("Av. Silva Jardim", "2494", "batel", "Curitiba", "PR", "Brasil", "80320-000");
                var email = new Email("acastilhofilho@outlook.com");
                var person = new Person(Guid.NewGuid(), name, email, phone, address);
                Persons.InsertOneAsync(person);
            }
        }
        private async Task<bool> CollectionExistsAsync(string collectionName)
        {
            var filter = new BsonDocument("name", collectionName);

            var collections = await database.ListCollectionsAsync(new ListCollectionsOptions { Filter = filter });

            return await collections.AnyAsync();
        }

        private void CreateIndexes()
        {
            var personBuilder = Builders<Person>.IndexKeys;
            var indexId = new CreateIndexModel<Person>(personBuilder.Ascending(x => x.Id));
            var indexEmail = new CreateIndexModel<Person>(personBuilder.Ascending(x => x.Email.Address));
            Persons.Indexes.CreateOne(indexId);
            Persons.Indexes.CreateOne(indexEmail);
        }

        public IMongoCollection<Person> Persons
        {
            get
            {
                return database.GetCollection<Person>("Persons");
            }
        }
    }
}
