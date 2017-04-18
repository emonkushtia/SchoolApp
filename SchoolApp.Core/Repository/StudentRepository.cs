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

    public class StudentRepository : IRepository<Student>
   {
        private readonly DataContext context;

        public StudentRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<PagedListResult<Student>> GetPagedList(PageableListQuery query)
        {
            var students = await this.context.Students

                    // .Where("name.Contains(@0)", "Em")
                    .OrderBy("name asc")
                    .Skip(query.Offset.GetValueOrDefault())
                    .Take(query.Limit)
                    .ToListAsync();

            var count = await this.context.Students.CountAsync();
            return new PagedListResult<Student>(students, count);
        }

        public async Task<Student> Get(int id)
        {
            return await this.context.Students.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Save(Student student)
        {
            if (student.Id == default(int))
            {
                this.context.Students.Add(student);
            }

            await this.context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var student = await this.context.Students.FirstOrDefaultAsync(x => x.Id == id);
            if (student != null)
            {
                this.context.Students.Remove(student);
                await this.context.SaveChangesAsync();
            }
        }
   }
}
