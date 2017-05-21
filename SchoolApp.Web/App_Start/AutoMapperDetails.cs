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

                     cfg.CreateMap<Enrollment, StudentItem>()
                         .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.StudentId))
                         .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Student.Email))
                         .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Student.Name))
                         .ForMember(dest => dest.Roll, opt => opt.MapFrom(src => src.Student.Roll));

                     cfg.CreateMap<Course, CourseItem>()
                         .ForMember(dest => dest.StudentItems, opt => opt.MapFrom(src => src.Enrollments));
                 });
        }
    }
}