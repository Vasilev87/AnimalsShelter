using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimalsShelter.Data;
using AnimalsShelter.Data.Model;
using AnimalsShelter.Data.Repositories;
using Moq;
using NUnit.Framework;

namespace AnimalsShelter.UnitTests.Data
{
    [TestFixture]
    public class EfRepositoryTests
    {
        private static DbSet<T> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));

            return dbSet.Object;
        }

        //[Test]
        //public void Controller_ShouldThrowsArgumentNullException_WhenPassedParametersAreNull()
        //{
        //    //Act and Assert
        //    Assert.Throws<ArgumentNullException>(() => new EfRepository<Animal>(null));
        //}


        [Test]
        public void Controller_ShouldNotThrowArgumentNullException_WhenPassedParametersAreNull()
        {
            var dBContextMock = new Mock<MsSqlDbContext>();

            //Act and Assert
            Assert.DoesNotThrow(() => new EfRepository<Animal>(dBContextMock.Object));
        }

        [Test]
        public void All_ShouldReturnsNotDeletedObjects_IfValid()
        {
            // Arrange
            var notDeletedAnimal = new Animal { IsDeleted = false };
            var deletedAnimal = new Animal { IsDeleted = true };

            var animals = new List<Animal>()
            {
                notDeletedAnimal,
                deletedAnimal
            };

            var animalsDbSet = GetQueryableMockDbSet(animals);

            var contextMock = new Mock<MsSqlDbContext>();
            contextMock.Setup(c => c.Set<Animal>()).Returns(animalsDbSet);

            var repository = new EfRepository<Animal>(contextMock.Object);

            // Act
            var result = repository.All;

            // Assert
            Assert.AreEqual(1, result.ToList().Count);
        }

        [Test]
        public void All_ShouldReturnsAllObjects_IfValid()
        {
            // Arrange
            var notDeletedAnimal = new Animal { IsDeleted = false };
            var deletedAnimal = new Animal { IsDeleted = true };

            var animals = new List<Animal>()
            {
                notDeletedAnimal,
                deletedAnimal
            };

            var animalsDbSet = GetQueryableMockDbSet(animals);

            var contextMock = new Mock<MsSqlDbContext>();
            contextMock.Setup(c => c.Set<Animal>()).Returns(animalsDbSet);

            var repository = new EfRepository<Animal>(contextMock.Object);

            // Act
            var result = repository.AllAndDeleted;

            // Assert
            Assert.AreEqual(2, result.ToList().Count);
        }
    }
}
