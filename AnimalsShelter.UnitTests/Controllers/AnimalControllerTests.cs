using AutoMapper;
using Moq;
using NUnit.Framework;
using AnimalsShelter.Data.Model;
using AnimalsShelter.Services.Contracts;
using AnimalsShelter.Web.Controllers;
using AnimalsShelter.Web.WebUtils.Contracts;
using AnimalsShelter.Web.ViewModels.Animals;
using System;
using System.Collections.Generic;
using System.Linq;
using TestStack.FluentMVCTesting;

namespace AnimalsShelter.UnitTests.Controllers
{
    public class AnimalControllerTests
    {
        [Test]
        public void Controller_ShouldThrowArgumentNullException_WhenProviderIsNull()
        {
            //Arrange
            var mapperMock = new Mock<IMapper>();
            var userServiceMock = new Mock<IUsersService>();
            var animalServiceMock = new Mock<IAnimalsService>();

            // Act && Assert
            Assert.Throws<ArgumentNullException>(() =>
                new AnimalController(null, mapperMock.Object, userServiceMock.Object, animalServiceMock.Object));
        }

        [Test]
        public void Controller_ShoudThrowArgumetnNullException_WhenMapperIsNull()
        {
            // Arrange
            var verificationMock = new Mock<IVerificationProvider>();
            var userServiceMock = new Mock<IUsersService>();
            var animalServiceMock = new Mock<IAnimalsService>();

            // Act && Assert
            Assert.Throws<ArgumentNullException>(() =>
                new AnimalController(verificationMock.Object, null, userServiceMock.Object, animalServiceMock.Object));
        }

        [Test]
        public void Controller_ShoudThrowArgumetnNullException_WhenUserServiceIsNull()
        {
            // Arrange
            var verificationMock = new Mock<IVerificationProvider>();
            var mapperMock = new Mock<IMapper>();
            var animalServiceMock = new Mock<IAnimalsService>();

            // Act && Assert
            Assert.Throws<ArgumentNullException>(() =>
                new AnimalController(verificationMock.Object, mapperMock.Object, null, animalServiceMock.Object));
        }

        [Test]
        public void Cotroller_ShouldThrowArgumentNullException_WhenAnimalServiceIsNull()
        {
            // Arrange
            var verificationMock = new Mock<IVerificationProvider>();
            var mapperMock = new Mock<IMapper>();
            var userServiceMock = new Mock<IUsersService>();

            // Act && Assert
            Assert.Throws<ArgumentNullException>(() =>
                new AnimalController(verificationMock.Object, mapperMock.Object, userServiceMock.Object, null));
        }

        //[Test]
        //public void Computer_ShouldReturnsTrue_WhenViewResult_IsValid()
        //{
        //    // Arrange
        //    Mapper.Initialize(cfg =>
        //    {
        //        cfg.CreateMap<Computer, ComputerViewModel>();
        //        cfg.CreateMap<ComputerViewModel, Computer>();
        //    });

        //    var mockedProvider = new Mock<IVerificationProvider>();
        //    var mockedMapper = new Mock<IMapper>();
        //    var mockedUsersService = new Mock<IUsersService>();
        //    var mockedComputersService = new Mock<IComputersService>();
        //    var mockedLaptopsService = new Mock<ILaptopsService>();
        //    var mockedDisplaysService = new Mock<IDisplaysService>();

        //    var controller = new DeviceController(mockedProvider.Object, mockedMapper.Object, mockedUsersService.Object,
        //        mockedComputersService.Object, mockedLaptopsService.Object, mockedDisplaysService.Object);

        //    // Act
        //    var computer = new Computer
        //    {
        //        Id = Guid.NewGuid()
        //    };
        //    var computersCollection = new List<Computer>() { computer };

        //    mockedComputersService.Setup(c => c.GetAll()).Returns(computersCollection.AsQueryable());


        //    //Assert
        //    controller
        //        .WithCallTo(c => c.Computer(computer.Id))
        //        .ShouldRenderView("Computer");
        //}


    }
}
