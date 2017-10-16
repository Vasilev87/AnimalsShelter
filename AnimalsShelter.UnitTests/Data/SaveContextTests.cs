using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimalsShelter.Data;
using AnimalsShelter.Data.SaveContext;
using Moq;
using NUnit.Framework;

namespace AnimalsShelter.UnitTests.Data
{
    [TestFixture]
    public class SaveContextTests
    {
        [Test]
        public void Controller_ShouldThrowsArgumentNullException_WhenPassedParametersAreNull()
        {
            var mockedDbContext = new Mock<MsSqlDbContext>();

            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => new SaveContext(null));
        }

        [Test]
        public void Controller_ShouldNotThrowArgumentNullException_WhenPassedParametersAreNull()
        {
            var mockedDbContext = new Mock<MsSqlDbContext>();

            //Act and Assert
            Assert.DoesNotThrow(() => new SaveContext(mockedDbContext.Object));
        }

        [Test]
        public void Commit_ShouldCallSaveChangesToDatabaseContext()
        {
            // Arrange
            var mockedDbContext = new Mock<MsSqlDbContext>();
            mockedDbContext.Setup(x => x.SaveChanges());

            var saveContext = new SaveContext(mockedDbContext.Object);

            // Act
            saveContext.Commit();

            // Assert
            mockedDbContext.Verify(dbc => dbc.SaveChanges(), Times.Once);
        }
    }
}
