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

            // builder.RegisterType<GenericRepository<Student>>().As<IRepository<Student>>();
            //builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IRepository<>));
           // builder.RegisterType<GenericRepository<>>().As<IRepository<>>();
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IRepository<>));
            // Set the dependency resolver to be Autofac.
            var container = builder.Build();

            var dataContext = container.Resolve<DataContext>();
            var srr = container.Resolve<IRepository<Student>>();
            var genericRepository = container.Resolve<IRepository<Student>>();
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
                        ;
                    });

            var student = dataContext.Students.First(x => x.Id == 1);
            var stu = Mapper.Map<StudentItem>(student);

            var query = new PageableListQuery();
            var students = genericRepository.GetPagedList(query).Result;
            var ssdd = Mapper.Map<IEnumerable<Student>, IEnumerable<StudentItem>>(students.List);
            new PagedListResult<StudentItem>(ssdd, 20);
            PagedListResult<StudentItem> listDest =
                Mapper.Map<PagedListResult<Student>, PagedListResult<StudentItem>>(students);
            Console.ReadLine();
        }
    }
}
