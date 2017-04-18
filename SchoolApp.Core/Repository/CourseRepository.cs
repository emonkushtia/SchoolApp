namespace SchoolApp.Core.Repository
{
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Dynamic;
    using System.Threading.Tasks;

    using DataAccess;
    using DataAccess.DomainObjects;

    using Infastucture;
    using Interfaces;

    public class CourseRepository : IRepository<Course>
    {
        private readonly DataContext context;

        public CourseRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<PagedListResult<Course>> GetPagedList(PageableListQuery query)
        {
            var courses = await this.context.Courses
                    .OrderBy("name asc")
                    .Skip(query.Offset.GetValueOrDefault())
                    .Take(query.Limit)
                    .ToListAsync();

            var count = await this.context.Courses.CountAsync();
            return new PagedListResult<Course>(courses, count);
        }

        public async Task<Course> Get(int id)
        {
            return await this.context.Courses.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Save(Course course)
        {
            if (course.Id == default(int))
            {
                this.context.Courses.Add(course);
            }

            await this.context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var course = await this.context.Courses.FirstOrDefaultAsync(x => x.Id == id);
            if (course != null)
            {
                this.context.Courses.Remove(course);
                await this.context.SaveChangesAsync();
            }
        }
    }
}
