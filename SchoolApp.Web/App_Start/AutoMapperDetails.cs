namespace SchoolApp.Web
{
    using System;

    using AutoMapper;

    using Core.DataTransferObjects;
    using DataAccess.DomainObjects;

    public static class AutoMapperDetails
    {
        public static void Maps()
        {
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
                 });
        }
    }
}