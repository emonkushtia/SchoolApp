namespace SchoolApp.Web.Controllers
{
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;

    using AutoMapper;

    using Core.DataTransferObjects;
    using Core.Infastucture;
    using Core.Interfaces;

    using SchoolApp.DataAccess.DomainObjects;

    [RoutePrefix("api/courses")]
    public class CourseController : ApiController
    {
        private readonly IRepository<Course> repository;

        public CourseController(IRepository<Course> repository)
        {
            this.repository = repository;
        }

        [Route("")]
        public async Task<HttpResponseMessage> Get(PageableListQuery query)
        {
            query = new PageableListQuery();
            var courses = await this.repository.GetPagedList(query);
            var list = Mapper.Map<IEnumerable<Course>, IEnumerable<CourseItem>>(courses.List);

            return this.Request.CreateResponse(
                HttpStatusCode.OK, new PagedListResult<CourseItem>(list, courses.Count));
        }

        [Route("")]
        public async Task<HttpResponseMessage> Post(CourseCreateItem courseItem)
        {
            var course = Mapper.Map<Course>(courseItem);
            await this.repository.Save(course);
            return this.Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("{id:int}")]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            await this.repository.Delete(id);
            return this.Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
