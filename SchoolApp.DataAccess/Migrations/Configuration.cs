namespace SchoolApp.DataAccess.Migrations
{
    using DomainObjects;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SchoolApp.DataAccess.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SchoolApp.DataAccess.DataContext context)
        {
            //IList<Course> defaultCourses = new List<Course>();

            //defaultCourses.Add(new Course() { Name = "Bangla 1", Code = "1001",IsDelete = false });
            //defaultCourses.Add(new Course() { Name = "English 1", Code = "1002", IsDelete = false });
            //defaultCourses.Add(new Course() { Name = "Math", Code = "1003", IsDelete = false });
            //defaultCourses.Add(new Course() { Name = "Bangla 2", Code = "1004", IsDelete = false });
            //defaultCourses.Add(new Course() { Name = "English 2", Code = "1005", IsDelete = false });
            //defaultCourses.Add(new Course() { Name = "Physics", Code = "1006", IsDelete = false });
            //defaultCourses.Add(new Course() { Name = "Chemistry", Code = "1007", IsDelete = false });
            //defaultCourses.Add(new Course() { Name = "Sociology", Code = "1008", IsDelete = false });
            //defaultCourses.Add(new Course() { Name = "Religion", Code = "1009", IsDelete = false });

            //foreach (Course course in defaultCourses)
            //    context.Courses.Add(course);

            //base.Seed(context);
        }
    }
}
