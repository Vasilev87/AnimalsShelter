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
    [TestFixture]
    public class AnimalControllerTests
    {
        //[Test]
        //public void Controller_ShouldThrowArgumentNullException_WhenProviderIsNull()
        //{
        //    //Arrange
        //    var mapperMock = new Mock<IMapper>();
        //    var userServiceMock = new Mock<IUsersService>();
        //    var animalServiceMock = new Mock<IAnimalsService>();

        //    // Act && Assert
        //    Assert.Throws<ArgumentNullException>(() =>
        //        new AnimalController(null, mapperMock.Object, userServiceMock.Object, animalServiceMock.Object));
        //}

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

        // Single Animal
        [Test]
        public void Animal_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Animal, AnimalsViewModel>();
                cfg.CreateMap<AnimalsViewModel, Animal>();
            });

            var verificationProviderMock = new Mock<IVerificationProvider>();
            var mapperMock = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var animalServiceMock = new Mock<IAnimalsService>();

            var animalController = new AnimalController(verificationProviderMock.Object, mapperMock.Object, mockedUsersService.Object, animalServiceMock.Object);

            // Act
            var animal = new Animal
            {
                Id = Guid.NewGuid()
            };
            var animalsCollection = new List<Animal>() { animal };

            animalServiceMock.Setup(c => c.GetAll()).Returns(animalsCollection.AsQueryable());


            //Assert
            animalController.WithCallTo(c => c.Animal(animal.Id))
                .ShouldRenderView("Animal");
        }

        // All Animals
        [Test]
        public void Animals_ShouldReturnTrue_WhenViewResult_IsValid()
        {
            // Arrange
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Animal, AnimalsViewModel>();
                cfg.CreateMap<AnimalsViewModel, Animal>();
            });

            var verificationProviderMock = new Mock<IVerificationProvider>();
            var mapperMock = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();
            var animalServiceMock = new Mock<IAnimalsService>();

            var animalController = new AnimalController(verificationProviderMock.Object, mapperMock.Object, mockedUsersService.Object, animalServiceMock.Object);

            //Act and Assert
            animalController.WithCallTo(x => x.Animals(1))
                .ShouldRenderView("Animals");
        }

        [Test]
        public void AddAnimalForAdoptionGET_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            var verificationMock = new Mock<IVerificationProvider>();
            var mapperMock = new Mock<IMapper>();
            var userServiceMock = new Mock<IUsersService>();
            var animalServiceMock = new Mock<IAnimalsService>();

            var adminPanelController = new AnimalController(verificationMock.Object, mapperMock.Object, userServiceMock.Object, animalServiceMock.Object);

            // Act and Assert
            adminPanelController.WithCallTo(c => c.AddFoundAnimal())
                .ShouldRenderView("AddFoundAnimal");
        }

        //[Test]
        //public void AddAnimalPostRequire_ShouldReturnsTrue_WhenViewResult_IsValid()
        //{
        //    // Arrange
        //    Mapper.Initialize(cfg =>
        //    {
        //        cfg.CreateMap<Animal, AnimalsViewModel>();
        //        cfg.CreateMap<AnimalsViewModel, Animal>();
        //    });
            
        //    var verificationMock = new Mock<IVerificationProvider>();
        //    var mapperMock = new Mock<IMapper>();
        //    var userServiceMock = new Mock<IUsersService>();
        //    var animalServiceMock = new Mock<IAnimalsService>();

        //    var adminPanelController = new AnimalController(verificationMock.Object, mapperMock.Object, userServiceMock.Object, animalServiceMock.Object);

        //    // Act
        //    var animal = new Animal
        //    {
        //        Id = Guid.NewGuid()
        //    };

        //    var user = new User
        //    {
        //        Id = "123"
        //    };

        //    var usersCollection = new List<User>() { user };

        //    verificationMock.Setup(x => x.CurrentUserId).Returns(user.Id);
        //    userServiceMock.Setup(c => c.GetAll()).Returns(usersCollection.AsQueryable());

        //    animalServiceMock.Setup(c => c.Add(animal));

        //    //Assert
        //    adminPanelController.WithCallTo(c => c.AddAnimalForAdoption(animal))
        //        .ShouldRedirectTo((ManageController c) => c.Index(null, 1));
        //}
    }
}
