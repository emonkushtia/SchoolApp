namespace SchoolApp.DataAccess
{
    using System.Data.Entity;

    using DomainObjects;

    public class DataContext : DbContext
    {
        public DataContext() : base("SchoolAppContext")
        {
        }

        public virtual IDbSet<Student> Students { get; set; }

        public virtual IDbSet<Course> Courses { get; set; }
    }
}
