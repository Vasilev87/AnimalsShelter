using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimalsShelter.Data;
using Moq;
using NUnit.Framework;

namespace AnimalsShelter.UnitTests.Data
{
    [TestFixture]
    public class MsSqlDbContextTests
    {
        [Test]
        public void Create_ShouldReturnNewInstanceOfMsSqlDbContextClass()
        {
            // Arrange and Act
            var result = MsSqlDbContext.Create();

            // Assert
            Assert.IsInstanceOf<MsSqlDbContext>(result);
            Assert.AreEqual("MsSqlDbContext", result.GetType().Name);
        }

        [Test]
        public void MsSqlDbContext_ShouldReturnNewCollectionOfComputers()
        {
            // Arrange
            var expected = 1;
            var mockedDbContext = new Mock<MsSqlDbContext>();

            // Act
            mockedDbContext.Setup(x => x.SaveChanges()).Returns(expected);
            var context = mockedDbContext.Object;
            var actual = context.SaveChanges();


            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
