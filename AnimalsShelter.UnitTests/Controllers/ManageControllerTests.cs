using System;
using AnimalsShelter.Data.Model;
using AnimalsShelter.Services.Contracts;
using AnimalsShelter.Web.Controllers;
using AnimalsShelter.Web.Models;
using AnimalsShelter.Web.ViewModels.Animals;
using AnimalsShelter.Web.WebUtils.Contracts;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;
//using Microsoft.AspNet.Identity.EntityFramework;

namespace AnimalsShelter.UnitTests.Controllers
{
    [TestFixture]
    public class ManageControllerTests
    {
        [Test]
        public void Controller_ShouldThrowArgumentNullException_WhenProviderIsNull()
        {
            //Arrange
            var mapperMock = new Mock<IMapper>();
            var userServiceMock = new Mock<IUsersService>();
            var animalServiceMock = new Mock<IAnimalsService>();

            Assert.Throws<ArgumentNullException>(() => new ManageController(null, mapperMock.Object, userServiceMock.Object, animalServiceMock.Object));
        }
        [Test]
        public void Controller_ShouldThrowArgumentNullException_WhenMapperIsNull()
        {
            //Arrange
            var verificationMock = new Mock<IVerificationProvider>();
            var userServiceMock = new Mock<IUsersService>();
            var animalServiceMock = new Mock<IAnimalsService>();

            Assert.Throws<ArgumentNullException>(() =>
            new ManageController(verificationMock.Object, null, userServiceMock.Object, animalServiceMock.Object));
        }

        [Test]
        public void Controller_ShouldThrowArgumentNullException_WhenUsersServiceIsNull()
        {
            //Arrange
            var verificationMock = new Mock<IVerificationProvider>();
            var mapperMock = new Mock<IMapper>();
            var animalServiceMock = new Mock<IAnimalsService>();

            Assert.Throws<ArgumentNullException>(() =>
            new ManageController(verificationMock.Object, mapperMock.Object, null, animalServiceMock.Object));
        }

        [Test]
        public void Controller_ShouldThrowArgumentNullException_WhenAnimalsServiceIsNull()
        {
            //Arrange
            var verificationMock = new Mock<IVerificationProvider>();
            var mapperMock = new Mock<IMapper>();
            var animalServiceMock = new Mock<IUsersService>();

            Assert.Throws<ArgumentNullException>(() =>
            new ManageController(verificationMock.Object, mapperMock.Object, animalServiceMock.Object, null));
        }

        [Test]
        public void Index_ShouldReturnTrue_WhenViewIsValid()
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

            var manageController = new ManageController(verificationMock.Object, mapperMock.Object, userServiceMock.Object, animalServiceMock.Object);

            var id = "123";

            verificationMock.Setup(v => v.CurrentUserId).Returns(id);

            // Act
            manageController.Index(null, 1);


            //Assert
            manageController
             .WithCallTo(c => c.ChangePassword())
             .ShouldRenderDefaultView();
        }

        [Test]
        public void Index_ShouldReturnTrue_WhenAnimalsService_IsCalled()
        {
            var id = "123";

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

            var manageController = new ManageController(verificationMock.Object, mapperMock.Object, userServiceMock.Object, animalServiceMock.Object);


            verificationMock.Setup(v => v.CurrentUserId).Returns(id);

            // Act
            manageController.Index(null, 1);


            //Assert
            animalServiceMock.Verify(x => x.GetAll(), Times.Once);
        }

        [Test]
        public void ChangePasswordGET_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            //Arrange
            var manageController = new ManageController();

            // Assert
            manageController.WithCallTo(c => c.ChangePassword())
                .ShouldRenderView("ChangePassword");
        }

        [Test]
        public void ChangePasswordPOST_ShouldRenderView_WhenIsNotValid()
        {
            // Arrange
            var manageController = new ManageController();
            var viewModel = new ChangePasswordViewModel();

            manageController.ModelState.AddModelError("wrorng model", "Error");

            // Act and Assert
            manageController.WithCallTo(c => c.ChangePassword(viewModel))
                .ShouldRenderDefaultView();
        }

        [Test]
        public void ChangePasswordPostRequest_ShouldCallVerificationChangePassword_WhenIsValid()
        {
            var id = "123";

            // Arrange
            var verificationMock = new Mock<IVerificationProvider>();
            var mapperMock = new Mock<IMapper>();
            var userServiceMock = new Mock<IUsersService>();
            var animalServiceMock = new Mock<IAnimalsService>();

            var manageController = new ManageController(verificationMock.Object, mapperMock.Object, userServiceMock.Object, animalServiceMock.Object);
            
            var viewModel = new ChangePasswordViewModel
            {
                OldPassword = "oldPassword",
                NewPassword = "newPassword"
            };
            var user = new Mock<User>();

            verificationMock.Setup(v => v.CurrentUserId).Returns(id);
            user.Object.Id = id;
            verificationMock.Setup(x => x.ChangePassword(id, viewModel.OldPassword, viewModel.NewPassword)).Returns(IdentityResult.Success);

            manageController.ChangePassword(viewModel);

            verificationMock.Verify(v => v.ChangePassword(id, viewModel.OldPassword, viewModel.NewPassword), Times.Once);

        }

        [Test]
        public void ChangePasswordPostRequest_ShouldRedirects_WhenIsValid()
        {
            var id = "123";

            // Arrange
            var verificationMock = new Mock<IVerificationProvider>();
            var mapperMock = new Mock<IMapper>();
            var userServiceMock = new Mock<IUsersService>();
            var animalServiceMock = new Mock<IAnimalsService>();

            var manageController = new ManageController(verificationMock.Object, mapperMock.Object, userServiceMock.Object, animalServiceMock.Object);

            var viewModel = new ChangePasswordViewModel
            {
                OldPassword = "oldPassword",
                NewPassword = "newPassword"
            };

            verificationMock.Setup(v => v.CurrentUserId).Returns(id);
            verificationMock.Setup(x => x.ChangePassword(id, viewModel.OldPassword, viewModel.NewPassword)).Returns(IdentityResult.Success);

            // Act and Assert
            manageController
                .WithCallTo(c => c.ChangePassword(viewModel))
                .ShouldRedirectTo(x => x.Index(null, 1));
        }
    }
}
