using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using PrBanco.API.ViewModels;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PrBanco.Tests.IntegrationTests
{
    [TestClass]
    public class PersonControllerTest
    {

        [ClassInitialize()]
        public static void TestInitialize(TestContext context)
        {
            EnvironmentTest.CreateServer();
        }

        [TestMethod]
        public async Task RegisterPersonShouldReturnOk()
        {
            //Arrange            
            var viewModel = new PersonViewModel()
            {
                EmailAddress = $"{Guid.NewGuid()}@hotmail.com",
                FirstName = "André",
                LastName = "Castilho",
                PhoneNumber = "41999308113",
                Address = new AddressViewModel()
            };

            var content = new StringContent(JsonConvert.SerializeObject(viewModel), Encoding.UTF8, "application/json");

            //Act
            var response = await EnvironmentTest.Client.PostAsync("person-management", content);
            var result = await response.Content.ReadAsStringAsync();

            //Assert
            response.EnsureSuccessStatusCode();
        }

    }
}
