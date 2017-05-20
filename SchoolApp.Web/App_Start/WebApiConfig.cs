namespace SchoolApp.Web
{
    using System;
    using System.Reflection;
    using System.Web.Http;

    using Autofac;
    using Autofac.Integration.WebApi;

    using AutoMapper;

    using Core.DataTransferObjects;
    using Core.Interfaces;
    using Core.Repository;
    using Core.Service;

    using DataAccess;
    using DataAccess.DomainObjects;

    using Newtonsoft.Json.Serialization;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();

            ResolveDependency(config);
        }

        private static void ResolveDependency(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<DataContext>().InstancePerRequest();

            // builder.RegisterType<StudentRepositoryService>().As<IRepositoryService<StudentItem, StudentCreateItem>>();
            // builder.RegisterType<CourseRepositoryService>().As<IRepositoryService<CourseItem, CourseCreateItem>>();

            // builder.RegisterType<StudentRepository>().As<IRepository<Student>>();
            // builder.RegisterType<CourseRepository>().As<IRepository<Course>>();
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IRepository<>));

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // OPTIONAL: Register the Autofac filter provider.
            builder.RegisterWebApiFilterProvider(config);

            AutoMapperDetails.Maps();

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
