using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimalsShelter.Data.Model;
using AnimalsShelter.Data.Repositories;
using AnimalsShelter.Data.SaveContext;
using AnimalsShelter.Services;
using Moq;
using NUnit.Framework;

namespace AnimalsShelter.UnitTests.Services
{
    [TestFixture]
    public class UserServiceTests
    {
        [Test]
        public void Service_ShouldThrowArgumentNullException_WhenEfRepositoryIsNull()
        {
            // Arrange
            var mockedSaveContext = new Mock<ISaveContext>();

            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => new UsersService(null, mockedSaveContext.Object));
        }

        [Test]
        public void Service_ShouldThrowArgumentNullException_WhenUnitOfWorkIsNull()
        {
            // Arrange
            var mockedEfRepository = new Mock<IEfRepository<User>>();

            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => new UsersService(mockedEfRepository.Object, null));
        }

        [Test]
        public void GetAll_ShouldReturnAllUsersFromDatabase_WhickAreNotDeleted()
        {
            // Arrange
            var mockedEfRepository = new Mock<IEfRepository<User>>();
            var mockedSaveContext = new Mock<ISaveContext>();

            mockedEfRepository.Setup(x => x.All);

            var service = new UsersService(mockedEfRepository.Object, mockedSaveContext.Object);

            // Act
            var result = service.GetAll();

            // Assert
            mockedEfRepository.Verify(x => x.All, Times.Once);
        }
    }
}
