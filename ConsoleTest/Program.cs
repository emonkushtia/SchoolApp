using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    using Autofac;

    using AutoMapper;

    using SchoolApp.Core.DataTransferObjects;
    using SchoolApp.Core.Infastucture;
    using SchoolApp.Core.Interfaces;
    using SchoolApp.Core.Repository;
    using SchoolApp.DataAccess;
    using SchoolApp.DataAccess.DomainObjects;

    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<DataContext>();
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IRepository<>));
            var container = builder.Build();
            var dataContext = container.Resolve<DataContext>();
            var studentRepository = container.Resolve<IRepository<Student>>();
            var courseRepository = container.Resolve<IRepository<Course>>();
            Mapper.Initialize(
                cfg =>
                {
                    cfg.CreateMap<StudentCreateItem, Student>().BeforeMap(
                        (s, d) =>
                        {
                            s.CoursesList.ForEach(
                                    x =>
                                    {
                                        d.Enrollments.Add(
                                                new Enrollment
                                                {
                                                    StudentId = s.Id,
                                                    CourseId = x,
                                                    EnrollmentDate = DateTime.Now
                                                });
                                    });
                        });

                    cfg.CreateMap<Student, StudentItem>()
                        .ForMember(dest => dest.CourseItems, opt => opt.MapFrom(src => src.Enrollments));

                    cfg.CreateMap<Enrollment, CourseItem>()
                            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CourseId))
                            .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Course.Code))
                            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Course.Name));

                    cfg.CreateMap<Enrollment, StudentItem>()
                        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.StudentId))
                        .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Student.Email))
                        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Student.Name))
                        .ForMember(dest => dest.Roll, opt => opt.MapFrom(src => src.Student.Roll));

                    cfg.CreateMap<Course, CourseItem>()
                        .ForMember(dest => dest.StudentItems, opt => opt.MapFrom(src => src.Enrollments));
                });

            var course = dataContext.Courses.ToList();
            var cs = Mapper.Map<IEnumerable<Course>, IEnumerable<CourseItem>>(course);

            var student = dataContext.Students.First(x => x.Id == 1);
            var stu = Mapper.Map<StudentItem>(student);

            var query = new PageableListQuery();
            var students = studentRepository.GetPagedList(query).Result;
            var ssdd = Mapper.Map<IEnumerable<Student>, IEnumerable<StudentItem>>(students.List);
            new PagedListResult<StudentItem>(ssdd, 20);
            PagedListResult<StudentItem> listDest =
                Mapper.Map<PagedListResult<Student>, PagedListResult<StudentItem>>(students);
            Console.ReadLine();
        }
    }
}
