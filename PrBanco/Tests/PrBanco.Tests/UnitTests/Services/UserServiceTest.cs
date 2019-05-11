using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PrBanco.API.Services;
using PrBanco.API.ViewModels;
using PrBanco.Domain.Entities;
using PrBanco.Domain.Repositories;
using PrBanco.Domain.ValueObjects;
using System;

namespace PrBanco.Tests.UnitTests.Services
{
    [TestClass]
    public class UserServiceTest
    {
        private PersonService _personService;
        public Mock<IMapper> mockMapper { get; set; }
        public Mock<IPersonRepository> mockRepository { get; set; }

        public UserServiceTest()
        {
            mockMapper = new Mock<IMapper>();
            mockRepository = new Mock<IPersonRepository>();

            _personService = new PersonService(mockMapper.Object, mockRepository.Object);

        }

        [TestMethod]
        public void ShoudCreatePerson()
        {
            //Arrange
            var name = new Name("André", "Castilho");
            var phone = new Phone("(41)99930-8113");
            var address = new Address("Av. Silva Jardim", "2494", "batel", "Curitiba", "PR", "Brasil", "80320-000");
            var email = new Email("acastilhofilho@outlook.com");
            var person = new Person(Guid.NewGuid(), name, email, phone, address);

            mockMapper.Setup(m => m.Map<Person>(It.IsAny<PersonViewModel>())).Returns(person);

            //Act
            _personService.Register(new PersonViewModel());

            //Assert
            mockRepository.Verify(r => r.Add(It.IsAny<Person>()), Times.Once);

        }

        [TestMethod]
        public void ShoudUpdatePerson()
        {
            //Arrange
            var name = new Name("André", "Castilho");
            var phone = new Phone("(41)99930-8113");
            var address = new Address("Av. Silva Jardim", "2494", "batel", "Curitiba", "PR", "Brasil", "80320-000");
            var email = new Email("acastilhofilho@outlook.com");
            var person = new Person(Guid.NewGuid(), name, email, phone, address);

            mockMapper.Setup(m => m.Map<Person>(It.IsAny<PersonViewModel>())).Returns(person);

            //Act
            _personService.Update(new PersonViewModel());

            //Assert
            mockRepository.Verify(r => r.GetByEmail(It.IsAny<string>()), Times.Once);
            mockRepository.Verify(r => r.Update(It.IsAny<Person>()), Times.Once);

        }

    }
}
