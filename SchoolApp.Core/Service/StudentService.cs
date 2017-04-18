namespace SchoolApp.Core.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using DataAccess.DomainObjects;

    using DataTransferObjects;
    using Infastucture;
    using Interfaces;

    public class StudentRepositoryService : IRepositoryService<StudentItem, StudentCreateItem>
    {
        private readonly IRepository<Student> repository;

        public StudentRepositoryService(IRepository<Student> repository)
        {
            this.repository = repository;
        }

        public async Task<PagedListResult<StudentItem>> GetPagedList(PageableListQuery query)
        {
            var studentItems = new List<StudentItem>();

           var students = await this.repository.GetPagedList(query);

            foreach (var item in students.List)
            {
                var studentItem = new StudentItem
                {
                    Id = item.Id, 
                    Name = item.Name, 
                    Roll = item.Roll, 
                    Email = item.Email
                };
                studentItems.Add(studentItem);
            }

            return new PagedListResult<StudentItem>(studentItems, studentItems.Count);
        }

        public async Task<StudentItem> Get(int id)
        {
            var student = await this.repository.Get(id);
            var studentItem = new StudentItem
                                  {
                                      Id = student.Id,
                                      Name = student.Name,
                                      Email = student.Email,
                                      Roll = student.Roll
                                  };
            student.Enrollments.ToList().ForEach(
                x =>
                    {
                        studentItem.CourseItems.Add(
                            new CourseItem { Id = x.Course.Id, Name = x.Course.Name, Code = x.Course.Code });
                    });
            return studentItem;
        }

        public async Task Save(StudentCreateItem studentItem)
        {
            var student = new Student
            {
                Id = studentItem.Id, 
                Name = studentItem.Name, 
                Roll = studentItem.Roll, 
                Email = studentItem.Email
            };

            foreach (var courseId in studentItem.CoursesList)
            {
                var enrollment = new Enrollment
                                     {
                                         StudentId = student.Id,
                                         CourseId = courseId,
                                         EnrollmentDate = DateTime.Now
                                     };

                student.Enrollments.Add(enrollment);
            }

            await this.repository.Save(student);
        }

        public async Task Update(StudentCreateItem studentItem)
        {
            var student = await this.repository.Get(studentItem.Id);
            if (student != null)
            {
                student.Name = studentItem.Name;
                student.Roll = studentItem.Roll;
                student.Email = studentItem.Email;

                student.Enrollments.Clear();
                foreach (var courseId in studentItem.CoursesList)
                {
                    var enrollment = new Enrollment
                    {
                        StudentId = student.Id,
                        CourseId = courseId,
                        EnrollmentDate = DateTime.Now
                    };

                    student.Enrollments.Add(enrollment);
                }

                await this.repository.Save(student);
            }
        }

        public async Task Delete(int id)
        {
           await this.repository.Delete(id);
        }
    }
}
