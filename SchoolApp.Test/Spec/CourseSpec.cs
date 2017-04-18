using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SeliseExamApp.Core.Repository;
using SeliseExamApp.DataAccess;
using SeliseExamApp.DataAccess.DomainObjects;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeliseExamApp.Test.Spec
{
    [TestClass]
    public class CourseSpec
    {
        [TestMethod]
        public void Course_Find_Test()
        {
            //Arrange
            const int expectedId = 1;
            var expected = new Course { CourseId = 2, Name = "English 1", Code = "1002" };
            var data = new List<Course>
            {
                expected,
                new Course { CourseId = 2, Name = "English 1", Code = "1002" }
            }.AsQueryable();

            var dbSetMock = new Mock<IDbSet<Course>>();
            dbSetMock.Setup(m => m.Provider).Returns(data.Provider);
            dbSetMock.Setup(m => m.Expression).Returns(data.Expression);
            dbSetMock.Setup(m => m.ElementType).Returns(data.ElementType);
            dbSetMock.Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var customDbContextMock = new Mock<DataContext>();
            customDbContextMock
                .Setup(x => x.Courses)
                .Returns(dbSetMock.Object);

            var classUnderTest = new CourseRepository(customDbContextMock.Object);

            //Action
            var actual = classUnderTest.Find(expected.CourseId);

            //Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.CourseId, actual.CourseId);
            Assert.AreEqual(expected.Name, actual.Name);
        }
    }
}
