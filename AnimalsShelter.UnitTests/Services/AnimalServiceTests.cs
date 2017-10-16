using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using AnimalsShelter.Data.Model;
using AnimalsShelter.Data.Repositories;
using AnimalsShelter.Data.SaveContext;
using AnimalsShelter.Services;

namespace AnimalsShelter.UnitTests.Services
{
    [TestFixture]
    public class AnimalServiceTests
    {
        [Test]
        public void Service_ShouldThrowArgumentNullException_WhenSaveContextIsNull()
        {
            // Arrange
            var mockedEfRepository = new Mock<IEfRepository<Animal>>();

            // Act && Assert
            Assert.Throws<ArgumentNullException>(() => new AnimalsService(mockedEfRepository.Object, null));
        }

        [Test]
        public void Service_ShouldThrowArgumentNullException_WhenEfRepositoryIsNull()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<ISaveContext>();

            // Act && Assert
            Assert.Throws<ArgumentNullException>(() => new AnimalsService(null, mockedUnitOfWork.Object));
        }

        [Test]
        public void AddAnimal_ShouldAddToDatabase_WhenInputIsCorrect()
        {
            // Arrange
            var mockedEfRepository = new Mock<IEfRepository<Animal>>();
            var mockedUnitOfWork = new Mock<ISaveContext>();
            var animal = new Animal();

            mockedEfRepository.Setup(x => x.Add(animal));

            var animalsService = new AnimalsService(mockedEfRepository.Object, mockedUnitOfWork.Object);

            // Act
            animalsService.Add(animal);

            // Assert
            mockedEfRepository.Verify(x => x.Add(animal), Times.Once);
        }

        [Test]
        public void UpdateAnimal_ShouldUpdateInfo_InDatabase()
        {
            // Arrange
            var mockedEfRepository = new Mock<IEfRepository<Animal>>();
            var mockedUnitOfWork = new Mock<ISaveContext>();
            var animal = new Animal();


            mockedEfRepository.Setup(x => x.Update(animal));

            var animalsService = new AnimalsService(mockedEfRepository.Object, mockedUnitOfWork.Object);


            // Act
            animalsService.Update(animal);

            // Assert
            mockedEfRepository.Verify(x => x.Update(animal), Times.Once);
        }

        [Test]
        public void DeleteAnimal_ShouldSetIsDeleted_True_InDatabase()
        {
            // Arrange
            var mockedEfRepository = new Mock<IEfRepository<Animal>>();
            var mockedUnitOfWork = new Mock<ISaveContext>();

            var animal = new Animal();

            mockedEfRepository.Setup(x => x.Delete(animal));

            var animalsService = new AnimalsService(mockedEfRepository.Object, mockedUnitOfWork.Object);

            // Act
            animalsService.Delete(animal);

            // Assert
            mockedEfRepository.Verify(x => x.Delete(animal), Times.Once);
        }

        [Test]
        public void GetAll_ShouldReturnAllAnimalsFromDatabase_WhickAreNotDeleted()
        {
            // Arrange
            var mockedEfRepository = new Mock<IEfRepository<Animal>>();
            var mockedUnitOfWork = new Mock<ISaveContext>();

            mockedEfRepository.Setup(x => x.All);

            var animalsService = new AnimalsService(mockedEfRepository.Object, mockedUnitOfWork.Object);

            // Act
            var result = animalsService.GetAll();

            // Assert
            mockedEfRepository.Verify(x => x.All, Times.Once);
        }
    }
}
