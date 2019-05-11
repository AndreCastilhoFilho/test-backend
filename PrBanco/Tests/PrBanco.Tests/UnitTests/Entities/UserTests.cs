using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrBanco.Domain.Entities;
using PrBanco.Domain.ValueObjects;
using System;
using System.Linq;

namespace PrBanco.Tests.UnitTests.Entities
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void UserShouldBeCreated()
        {
            //Arrange
            var name = new Name("André", "Castilho");
            var phone = new Phone("(41)99930-8113");
            var address = new Address("Av. Silva Jardim", "2494", "batel", "Curitiba", "PR", "Brasil", "80320-000");
            var email = new Email("acastilhofilho@outlook.com");

            //Act
            var person = new Person(Guid.NewGuid(), name, email, phone, address);

            //Assert
            Assert.IsTrue(person.Valid);
            Assert.AreEqual("André Castilho", person.Name.ToString());

        }

        [TestMethod]
        public void ShouldReturnErrorWhenEmailIsInvalid()
        {
            //Arrange
            var name = new Name("André", "Castilho");
            var phone = new Phone("(41)99930-8113");
            var address = new Address("Av. Silva Jardim", "2494", "batel", "Curitiba", "PR", "Brasil", "80320-000");
            var email = new Email("emailerrado.com");

            //Act
            var person = new Person(Guid.NewGuid(), name, email, phone, address);

            //Assert
            Assert.IsTrue(person.Invalid);
            Assert.IsTrue(person.Notifications.Count == 1);
            Assert.AreEqual(person.Notifications.First().Message, "E-mail inválido");
        }

        [TestMethod]
        public void ShouldReturnErrorWhenNameIsInvalid()
        {
            //Arrange
            var name = new Name("", "");
            var phone = new Phone("(41)99930-8113");
            var address = new Address("Av. Silva Jardim", "2494", "batel", "Curitiba", "PR", "Brasil", "80320-000");
            var email = new Email("acastilhofilho@outlook.com");

            //Act
            var person = new Person(Guid.NewGuid(), name, email, phone, address);

            //Assert
            Assert.IsTrue(person.Invalid);
            Assert.IsTrue(person.Notifications.Count == 3);

        }

    }
}
