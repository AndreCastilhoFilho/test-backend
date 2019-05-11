using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Reflection;
using System.Security.Authentication;

namespace PrBanco.Data.MongoDataAccess.Context
{
    public abstract class MongoContext
    {
        private readonly MongoClient mongoClient;
        protected readonly IMongoDatabase database;

        public MongoContext(IOptions<DbSettings> settings)
        {
            MongoClientSettings mongosettings = MongoClientSettings.FromUrl(
               new MongoUrl(settings.Value.ConnectionString));

            mongosettings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            mongoClient = new MongoClient(mongosettings);
            this.database = mongoClient.GetDatabase(settings.Value.Database);

            Map();
        }

        private void Map()
        {
            var assembly = Assembly.GetExecutingAssembly();

            var classMaps = assembly
                .GetTypes()
                .Where(t => t.BaseType != null && t.BaseType.IsGenericType &&
                            t.BaseType.GetGenericTypeDefinition() == typeof(MongoDbClassMap<>));

            foreach (var classMap in classMaps)
                Activator.CreateInstance(classMap);
        }
    }
}
