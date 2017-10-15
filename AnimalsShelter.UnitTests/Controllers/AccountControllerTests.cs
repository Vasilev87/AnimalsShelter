using NUnit.Framework;
using AnimalsShelter.Web.Controllers;
using System.Web.Mvc;
using Moq;
using AnimalsShelter.Web.WebUtils.Contracts;
using System;
using TestStack.FluentMVCTesting;
using AnimalsShelter.Web.Models;
using Microsoft.AspNet.Identity.Owin;
using AnimalsShelter.Data.Model;
using Microsoft.AspNet.Identity;

namespace AnimalsShelter.UnitTests.Controllers
{
    [TestFixture]
    public class AccountControllerTests
    {
        //[Test]
        //public void Login_ShouldReturnViewNotNull()
        //{
        //    //Arange
        //    AccountController accController = new AccountController();

        //    //Act
        //    ViewResult viewResult = accController.Login(It.IsAny<string>()) as ViewResult;

        //    //Assert
        //    Assert.IsNotNull(viewResult);
        //}

        [Test]
        public void LoginGetRequest_ShouldRedirects_WhenSignedUp()
        {
            // Arrange
            var returnUrl = "returnUrl";
            var verificationMock = new Mock<IVerificationProvider>();
            var accController = new AccountController(verificationMock.Object);

            // Act
            verificationMock.Setup(x => x.IsAuthenticated).Returns(true);

            // Assert
            accController
                .WithCallTo(c => c.Login(returnUrl))
                .ShouldRedirectTo((ShelterController c) => c.Index());
        }

        [Test]
        public void LoginPostRequst_ShouldRedirects_WhenSignedUp()
        {
            // Arrange
            var returnUrl = "returnUrl";
            var verificationMock = new Mock<IVerificationProvider>();
            var accController = new AccountController(verificationMock.Object);
            var loginViewModel = new LoginViewModel();

            // Act
            verificationMock.Setup(x => x.IsAuthenticated).Returns(true);

            // Assert
            accController.WithCallTo(c => c.Login(loginViewModel, returnUrl))
                .ShouldRedirectTo((ShelterController c) => c.Index());
        }

        [Test]
        public void LoginGetRequest_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            var returnUrl = "returnUrl";
            var verificationMock = new Mock<IVerificationProvider>();
            var accController = new AccountController(verificationMock.Object);

            // Act
            verificationMock.Setup(x => x.IsAuthenticated).Returns(false);

            // Assert
            accController.WithCallTo(c => c.Login(returnUrl))
                .ShouldRenderView("Login");
        }

        [Test]
        public void LoginPostRequest_ShouldRenderView_WhenIsNotValid()
        {
            // Arrange
            var returnUrl = "returnUrl";
            var loginViewModel = new LoginViewModel();
            var verificationMock = new Mock<IVerificationProvider>();
            var accController = new AccountController(verificationMock.Object);

            accController.ModelState.AddModelError("wrorng model", "Error");

            // Act and Assert
            accController.WithCallTo(c => c.Login(loginViewModel, returnUrl))
                .ShouldRenderDefaultView();
        }

        [Test]
        public void LoginPostRequest_ShouldSignUp_ValidUser()
        {
            // Arrange
            var returnUrl = "returnUrl";
            var verificationMock = new Mock<IVerificationProvider>();

            var loginViewModel = new LoginViewModel()
            {
                Password = "123456",
                Email = "myAcc@shelter.com",
                RememberMe = true
            };

            verificationMock.Setup(x => x.GetUserByEmail(loginViewModel.Email));

            verificationMock.Setup(v => v.SignInWithPassword(loginViewModel.Email, loginViewModel.Password, loginViewModel.RememberMe, It.IsAny<bool>()))
                .Returns(SignInStatus.Success);

            var accController = new AccountController(verificationMock.Object);

            // Act and Assert
            accController.WithCallTo(c => c.Login(loginViewModel, returnUrl))
                .ShouldRedirectTo(returnUrl);
        }

        [Test]
        public void LoginPostRequest_ShouldThrowError_ForNotValidUser()
        {
            // Arrange
            var returnUrl = "returnUrl";
            var verificationMock = new Mock<IVerificationProvider>();

            var loginViewModel = new LoginViewModel()
            {
                Password = "123456",
                Email = "myAcc@shelter.com",
                RememberMe = true
            };

            verificationMock.Setup(x => x.GetUserByEmail(loginViewModel.Email));


            verificationMock.Setup(v => v.SignInWithPassword(loginViewModel.Email, loginViewModel.Password, loginViewModel.RememberMe, It.IsAny<bool>()))
                .Returns(SignInStatus.Failure);

            var accController = new AccountController(verificationMock.Object);


            // Act and Assert
            accController.WithCallTo(c => c.Login(loginViewModel, returnUrl))
                .ShouldRenderDefaultView();
        }

        //[Test]
        //public void Register_ShouldReturnViewNotNull()
        //{
        //    //Arange
        //    AccountController accController = new AccountController();

        //    //Act
        //    var viewResult = accController.Register() as ViewResult;

        //    //Assert
        //    Assert.IsNotNull(viewResult);
        //}

        [Test]
        public void ShouldNotThrowArgumentNullException_WhenPassedParametersAreNull()
        {
            // Arrange
            var verificationMock = new Mock<IVerificationProvider>();

            // Act && Assert
            Assert.DoesNotThrow(() => new AccountController(verificationMock.Object));
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenPassedParametersAreNull()
        {
            // Act && Assert
            Assert.Throws<ArgumentNullException>(() => new AccountController(null));
        }

        [Test]
        public void RegisterGET_ShouldRedirects_WhenRegistered()
        {
            // Arrange
            var mockedVerification = new Mock<IVerificationProvider>();
            var accController = new AccountController(mockedVerification.Object);

            // Act
            mockedVerification.Setup(x => x.IsAuthenticated).Returns(true);

            // Assert
            accController.WithCallTo(c => c.Register())
                .ShouldRedirectTo((ShelterController c) => c.Index());
        }

        [Test]
        public void RegisterGET_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            var mockedVerification = new Mock<IVerificationProvider>();
            var accController = new AccountController(mockedVerification.Object);

            // Act
            mockedVerification.Setup(x => x.IsAuthenticated).Returns(false);

            // Assert
            accController.WithCallTo(c => c.Register())
                .ShouldRenderView("Register");
        }

        [Test]
        public void RegisterPostRequest_ShouldRedirect_WhenSuccessful()
        {
            // Arrange
            var verificationMock = new Mock<IVerificationProvider>();
            var accController = new AccountController(verificationMock.Object);
            var loginViewModel = new RegisterViewModel();

            // Act
            verificationMock.Setup(x => x.IsAuthenticated).Returns(true);

            // Assert
            accController.WithCallTo(c => c.Register(loginViewModel))
                .ShouldRedirectTo((ShelterController c) => c.Index());
        }

        [Test]
        public void RegisterPostRequest_ShouldRegister_NewUser()
        {
            // Arrange
            var verificationMock = new Mock<IVerificationProvider>();

            verificationMock.Setup(v => v.RegisterAndLoginUser(It.IsAny<User>(), It.IsAny<string>(),
                It.IsAny<bool>(), It.IsAny<bool>())).Returns(IdentityResult.Success);

            var accController = new AccountController(verificationMock.Object);
            var registerViewModel = new RegisterViewModel()
            {
                Password = "password",
                Email = "pesho@abv.bg"
            };

            accController.Register(registerViewModel);

            // Act and Assert
            accController.WithCallTo(c => c.Register(registerViewModel))
                .ShouldRedirectTo((ShelterController c) => c.Index());
        }

        [Test]
        public void LogOff_ShouldRedirects_WhenSuccessfulLogOff()
        {
            // Arrange
            var verificationMock = new Mock<IVerificationProvider>();
            var accController = new AccountController(verificationMock.Object);

            // Act
            verificationMock.Setup(x => x.IsAuthenticated).Returns(false);

            // Assert
            accController.WithCallTo(c => c.LogOff())
                .ShouldRedirectTo((ShelterController c) => c.Index());
        }

        [Test]
        public void LogOff_ShouldRedirect_WhenNotSuccessfulLogOff()
        {
            // Arrange
            var verificationMock = new Mock<IVerificationProvider>();
            var accController = new AccountController(verificationMock.Object);

            // Act
            verificationMock.Setup(x => x.IsAuthenticated).Returns(true);
            verificationMock.Setup(x => x.SignOut());

            // Assert
            accController.WithCallTo(c => c.LogOff())
                .ShouldRedirectTo((ShelterController c) => c.Index());
        }


    }
}
