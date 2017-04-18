namespace SchoolApp.Test.Spec
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    using Core.Repository;

    using DataAccess;
    using DataAccess.DomainObjects;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class StudentSpec
    {
        [TestMethod]
        public async Task Student_Find_Test()
        {
            // Arrange
            const int ExpectedId = 1;
            var expected = new Student
                               {
                                   Id = ExpectedId,
                                   Name = "Muna",
                                   Roll = "C021312312",
                                   Email = "engmunacse@gmail.com"
                               };
            var data = new List<Student>
                           {
                               expected,
                               new Student { Id = 2, Name = "Jame", Roll = "C02131432", Email = "jame@gmail.com" },
                               new Student { Id = 3, Name = "Emon", Roll = "C021312564", Email = "emon@gmail.com" },
                               new Student { Id = 4, Name = "Monjur", Roll = "C021312987", Email = "monjur@gmail.com" }
                           }.AsQueryable();
            

            var mockSet = new Mock<DbSet<Student>>();
            mockSet.As<IQueryable<Student>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Student>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Student>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Student>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var customDbContextMock = new Mock<DataContext>();
            customDbContextMock
                .Setup(x => x.Students)
                .Returns(mockSet.Object);

            var classUnderTest = new StudentRepository(customDbContextMock.Object);

            // Action
            var actual = await classUnderTest.Get(ExpectedId);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Name, actual.Name);
        }

        //[TestMethod]
        //public void Student_Save_Test()
        //{
        //    var student = new Student { Name = "Munaour Mehtab", Roll = "C021312312", Email = "engmunacse@gmail.com" };

        //    var dbSetMock = new Mock<IDbSet<Student>>();

        //    var customDbContextMock = new Mock<DataContext>();
        //    customDbContextMock
        //        .Setup(x => x.Students)
        //        .Returns(dbSetMock.Object);

        //    var classUnderTest = new StudentRepository(customDbContextMock.Object);

        //    classUnderTest.InsertOrUpdate(student);
        //    classUnderTest.Save();
        //}

        //[TestMethod]
        //public void Student_GetAll_Test()
        //{
        //    //Arrange
        //    var data = new List<Student>
        //    {
        //        new Student { StudentId = 1, Name = "Md. Munaour Mehtab", Roll = "C091505", Email = "engmunacse@gmail.com" },
        //        new Student { StudentId = 2, Name = "Jame",Roll="C02131432", Email="jame@gmail.com" },
        //        new Student { StudentId = 3, Name = "Emon",Roll="C021312564", Email="emon@gmail.com" },
        //        new Student { StudentId = 4, Name = "Monjur",Roll="C021312987", Email="monjur@gmail.com" }
        //    }.AsQueryable();

        //    var dbSetMock = new Mock<IDbSet<Student>>();
        //    dbSetMock.Setup(m => m.Provider).Returns(data.Provider);
        //    dbSetMock.Setup(m => m.Expression).Returns(data.Expression);
        //    dbSetMock.Setup(m => m.ElementType).Returns(data.ElementType);
        //    dbSetMock.Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        //    var customDbContextMock = new Mock<DataContext>();
        //    customDbContextMock
        //        .Setup(x => x.Students)
        //        .Returns(dbSetMock.Object);

        //    var classUnderTest = new StudentRepository(customDbContextMock.Object);

        //    //Action
        //    var actual = classUnderTest.GetAll();

        //    //Assert
        //    Assert.IsNotNull(actual);
        //    Assert.AreEqual(data.First().Name, actual.First().Name);
        //    Assert.AreNotEqual(data.Last().Email, actual.First().Name);
        //}

        //[TestMethod]
        //public void Student_GetByRoll_Test()
        //{
        //    //Arrange
        //    var data = new List<Student>
        //    {
        //        new Student { StudentId = 1, Name = "Md. Munaour Mehtab", Roll = "C091505", Email = "engmunacse@gmail.com" },
        //        new Student { StudentId = 2, Name = "Jame",Roll="C02131432", Email="jame@gmail.com" },
        //        new Student { StudentId = 3, Name = "Emon",Roll="C021312564", Email="emon@gmail.com" },
        //        new Student { StudentId = 4, Name = "Monjur",Roll="C021312987", Email="monjur@gmail.com" }
        //    }.AsQueryable();

        //    var dbSetMock = new Mock<IDbSet<Student>>();
        //    dbSetMock.Setup(m => m.Provider).Returns(data.Provider);
        //    dbSetMock.Setup(m => m.Expression).Returns(data.Expression);
        //    dbSetMock.Setup(m => m.ElementType).Returns(data.ElementType);
        //    dbSetMock.Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        //    var customDbContextMock = new Mock<DataContext>();
        //    customDbContextMock
        //        .Setup(x => x.Students)
        //        .Returns(dbSetMock.Object);

        //    var classUnderTest = new StudentRepository(customDbContextMock.Object);

        //    //Action
        //    var actual = classUnderTest.FindByRoll("C021312564");

        //    //Assert
        //    Assert.IsNotNull(actual);
        //    Assert.AreEqual("Emon", actual.Name);
        //    Assert.AreEqual("emon@gmail.com", actual.Email);
        //}
    }
}
