namespace SchoolApp.Core.Service
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using DataAccess.DomainObjects;

    using DataTransferObjects;
    using Infastucture;
    using Interfaces;

    public class CourseRepositoryService : IRepositoryService<CourseItem, CourseCreateItem>
    {
        private readonly IRepository<Course> repository;

        public CourseRepositoryService(IRepository<Course> repository)
        {
            this.repository = repository;
        }

        public async Task<PagedListResult<CourseItem>> GetPagedList(PageableListQuery query)
        {
            var lstCourse = await this.repository.GetPagedList(query);

            var lstCourseItem = new List<CourseItem>();
            foreach (var item in lstCourse.List)
            {
                var courseItem = new CourseItem()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Code = item.Code,
                    StudentItems = new List<StudentItem>()
                };

                foreach (var enrollment in item.Enrollments)
                {
                    var studentItem = new StudentItem()
                    {
                        Id = enrollment.Student.Id,
                        Name = enrollment.Student.Name,
                        Roll = enrollment.Student.Roll,
                        Email = enrollment.Student.Email
                    };
                    courseItem.StudentItems.Add(studentItem);
                }

                lstCourseItem.Add(courseItem);
            }

            return new PagedListResult<CourseItem>(lstCourseItem, lstCourseItem.Count);
        }

        public async Task<CourseItem> Get(int id)
        {
            var course = await this.repository.Get(id);
            var courseItem = new CourseItem
            {
                Id = course.Id,
                Name = course.Name,
                Code = course.Code
            };
            course.Enrollments.ToList().ForEach(
                x =>
                    {
                        courseItem.StudentItems.Add(
                            new StudentItem
                                {
                                    Id = x.Student.Id,
                                    Name = x.Student.Name,
                                    Email = x.Student.Email,
                                    Roll = x.Student.Roll
                                });
                    });
            return courseItem;
        }

        public async Task Save(CourseCreateItem courseItem)
        {
            var course = new Course()
            {
                Id = courseItem.Id,
                Name = courseItem.Name,
                Code = courseItem.Code
            };

            await this.repository.Save(course);
        }

        public async Task Update(CourseCreateItem courseItem)
        {
            var course = await this.repository.Get(courseItem.Id);
            if (course != null)
            {
                course.Name = courseItem.Name;
                course.Code = courseItem.Code;
                await this.repository.Save(course);
            }
        }

        public async Task Delete(int id)
        {
            await this.repository.Delete(id);
        }
    }
}
