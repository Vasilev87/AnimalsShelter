using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using AnimalsShelter.Data.Model;
using AnimalsShelter.Services.Contracts;
using AnimalsShelter.Web.Areas.Admin.Controllers;
using AnimalsShelter.Web.Models;
using AnimalsShelter.Web.ViewModels.Animals;
using AnimalsShelter.Web.WebUtils.Contracts;
using AutoMapper;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace AnimalsShelter.UnitTests.Controllers
{
    [TestFixture]
    public class AdminPanelControllerTests
    {
        [Test]
        public void Controller_ShouldThrowArgumentNullException_WhenHttpContextIsNull()
        {
            //Arrange
            var mapperMock = new Mock<IMapper>();
            var userServiceMock = new Mock<IUsersService>();
            var animalServiceMock = new Mock<IAnimalsService>();

            // Act && Assert
            Assert.Throws<ArgumentNullException>(() =>
                new AdminPanelController(null, mapperMock.Object, userServiceMock.Object, animalServiceMock.Object));
        }

        [Test]
        public void Controller_ShouldThrowArgumentNullException_WhenProviderIsNull()
        {
            //Arrange
            var verificationMock = new Mock<IVerificationProvider>();
            var userServiceMock = new Mock<IUsersService>();
            var animalServiceMock = new Mock<IAnimalsService>();

            // Act && Assert
            Assert.Throws<ArgumentNullException>(() =>
                new AdminPanelController(verificationMock.Object, null, userServiceMock.Object, animalServiceMock.Object));
        }

        [Test]
        public void Controller_ShouldThrowArgumentNullException_WhenMapperIsNull()
        {
            //Arrange
            var verificationMock = new Mock<IVerificationProvider>();
            var mapperMock = new Mock<IMapper>();
            var animalServiceMock = new Mock<IAnimalsService>();

            // Act && Assert
            Assert.Throws<ArgumentNullException>(() =>
                new AdminPanelController(verificationMock.Object, mapperMock.Object, null, animalServiceMock.Object));
        }

        [Test]
        public void Controller_ShouldThrowArgumentNullException_WhenUsersServiceIsNull()
        {
            //Arrange
            var verificationMock = new Mock<IVerificationProvider>();
            var mapperMock = new Mock<IMapper>();
            var userServiceMock = new Mock<IUsersService>();
            var animalServiceMock = new Mock<IAnimalsService>();

            // Act && Assert
            Assert.Throws<ArgumentNullException>(() =>
                new AdminPanelController(verificationMock.Object, mapperMock.Object, userServiceMock.Object, null));
        }

        [Test]
        public void Index_ShouldReturnsTrue_WhenAnimals_AreValid()
        {
            // Arrange
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Animal, AnimalsViewModel>();
                cfg.CreateMap<AnimalsViewModel, Animal>();

                cfg.CreateMap<User, UserViewModel>();
                cfg.CreateMap<UserViewModel, User>();
            });

            var verificationMock = new Mock<IVerificationProvider>();
            var mapperMock = new Mock<IMapper>();
            var userServiceMock = new Mock<IUsersService>();
            var animalServiceMock = new Mock<IAnimalsService>();
            
            var adminPanelController = new AdminPanelController(verificationMock.Object, mapperMock.Object, userServiceMock.Object, animalServiceMock.Object);

            // Act
            var animal = new Animal();
            var user = new User();

            var animalsCollection = new List<Animal>() { animal };
            var usersCollection = new List<User>() { user };


            animalServiceMock.Setup(c => c.GetAll()).Returns(animalsCollection.AsQueryable());

            userServiceMock.Setup(c => c.GetAll()).Returns
                (usersCollection.AsQueryable());

            //Assert
            adminPanelController.WithCallTo(c => c.Index())
                .ShouldRenderDefaultView();
        }

        [Test]
        public void Animals_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Animal, AnimalsViewModel>();
                cfg.CreateMap<AnimalsViewModel, Animal>();
            });

            var verificationMock = new Mock<IVerificationProvider>();
            var mapperMock = new Mock<IMapper>();
            var userServiceMock = new Mock<IUsersService>();
            var animalServiceMock = new Mock<IAnimalsService>();

            var adminPanelController = new AdminPanelController(verificationMock.Object, mapperMock.Object, userServiceMock.Object, animalServiceMock.Object);

            //Act and Assert
            adminPanelController.WithCallTo(c => c.Animals(1))
                .ShouldRenderView("Animals");
        }

        [Test]
        public void AddAnimalForAdoptionGetRequest_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            var verificationMock = new Mock<IVerificationProvider>();
            var mapperMock = new Mock<IMapper>();
            var userServiceMock = new Mock<IUsersService>();
            var animalServiceMock = new Mock<IAnimalsService>();

            var adminPanelController = new AdminPanelController(verificationMock.Object, mapperMock.Object, userServiceMock.Object, animalServiceMock.Object);

            // Act and Assert
            adminPanelController.WithCallTo(c => c.AddAnimalForAdoption())
                .ShouldRenderView("AddAnimalForAdoption");
        }

        [Test]
        public void AddAnimalForRehomeGetRequest_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            var verificationMock = new Mock<IVerificationProvider>();
            var mapperMock = new Mock<IMapper>();
            var userServiceMock = new Mock<IUsersService>();
            var animalServiceMock = new Mock<IAnimalsService>();

            var adminPanelController = new AdminPanelController(verificationMock.Object, mapperMock.Object, userServiceMock.Object, animalServiceMock.Object);

            // Act and Assert
            adminPanelController.WithCallTo(c => c.AddAnimalForRehome())
                .ShouldRenderView("AddAnimalForRehome");
        }

        [Test]
        public void AddLostAnimalGetRequest_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            var verificationMock = new Mock<IVerificationProvider>();
            var mapperMock = new Mock<IMapper>();
            var userServiceMock = new Mock<IUsersService>();
            var animalServiceMock = new Mock<IAnimalsService>();

            var adminPanelController = new AdminPanelController(verificationMock.Object, mapperMock.Object, userServiceMock.Object, animalServiceMock.Object);

            // Act and Assert
            adminPanelController.WithCallTo(c => c.AddLostAnimal())
                .ShouldRenderView("AddLostAnimal");
        }

        [Test]
        public void AddFoundAnimalGetRequest_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            var verificationMock = new Mock<IVerificationProvider>();
            var mapperMock = new Mock<IMapper>();
            var userServiceMock = new Mock<IUsersService>();
            var animalServiceMock = new Mock<IAnimalsService>();

            var adminPanelController = new AdminPanelController(verificationMock.Object, mapperMock.Object, userServiceMock.Object, animalServiceMock.Object);

            // Act and Assert
            adminPanelController.WithCallTo(c => c.AddFoundAnimal())
                .ShouldRenderView("AddFoundAnimal");
        }

        //[Test]
        //public void AddAnimalPostRequest_ShouldReturnsTrue_WhenViewResult_IsValid()
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

        //    var adminPanelController = new AdminPanelController(verificationMock.Object, mapperMock.Object, userServiceMock.Object, animalServiceMock.Object);

        //    //string filePath = Path.GetFullPath(@"AnimalsShelter.Web\UploadedFiles\testimage.jpg");
        //    //FileStream fileStream = new FileStream(filePath, FileMode.Open);

        //    var image = "testimage.jpg";
        //    var fullpath = "/test/testimage.jpg";

        //    var uploadedFile = new Mock<HttpPostedFileBase>();
        //    uploadedFile
        //        .Setup(f => f.ContentLength)
        //        .Returns(0);

        //    uploadedFile
        //        .Setup(f => f.FileName)
        //        .Returns(image);


        //    var server = new Mock<HttpServerUtilityBase>();
        //    server.Setup(s => s.MapPath(image)).Returns(fullpath);

        //    uploadedFile
        //        .Setup(f => f.SaveAs(fullpath));

        //    //uploadedFile
        //    //    .Setup(f => f.InputStream)
        //    //    .Returns(fileStream);

        //    // Act


        //   var animal = new Animal
        //    {
        //        Id = Guid.NewGuid(),
        //        ImagePath = "~/UploadedFiles/",
        //        Image = uploadedFile.Object
        //    };

        //    var user = new User
        //    {
        //        Id = "007"
        //    };

        //    var usersCollection = new List<User>() { user };

        //    verificationMock.Setup(x => x.CurrentUserId).Returns(user.Id);
        //    userServiceMock.Setup(c => c.GetAll()).Returns(usersCollection.AsQueryable());

        //    animalServiceMock.Setup(c => c.Add(animal));

        //    //Assert
        //    adminPanelController.WithCallTo(c => c.AddAnimalForAdoption(animal))
        //        .ShouldRedirectTo(c => c.Index());
        //}
    }
}
