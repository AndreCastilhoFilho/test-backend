using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using PrBanco.API;
using System.IO;
using System.Net.Http;

namespace PrBanco.Tests.IntegrationTests
{
    public static class EnvironmentTest
    {
        public static TestServer Server { get; set; }
        public static HttpClient Client { get; set; }
        public static IConfiguration Config { get; set; }
        public static IMongoDatabase Database { get; private set; }


        public static void CreateServer()
        {
            Server = new TestServer(
                new WebHostBuilder()
                    .UseEnvironment("Development")
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .UseUrls("http://localhost:64303")
                    .UseStartup<StartupTest>());

            Config = new ConfigurationBuilder()
                   .AddJsonFile("appsettings.Development.json")
                   .Build();

            var mongoClient = new MongoClient(Config.GetSection("MongoConnection")["ConnectionString"]);
            Database = mongoClient.GetDatabase(Config.GetSection("MongoConnection")["Database"]);

            Client = Server.CreateClient();

        }

    }
}
