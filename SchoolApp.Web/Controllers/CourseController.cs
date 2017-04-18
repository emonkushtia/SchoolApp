namespace SchoolApp.Web.Controllers
{
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;

    using Core.DataTransferObjects;
    using Core.Infastucture;
    using Core.Interfaces;

    [RoutePrefix("api/courses")]
    public class CourseController : ApiController
    {
        private readonly IRepositoryService<CourseItem, CourseCreateItem> repositoryService;

        public CourseController(IRepositoryService<CourseItem, CourseCreateItem> repositoryService)
        {
            this.repositoryService = repositoryService;
        }

        [Route("")]
        public async Task<HttpResponseMessage> Get(PageableListQuery query)
        {
            query = new PageableListQuery();
            var courses = await this.repositoryService.GetPagedList(query);
            return this.Request.CreateResponse(
                HttpStatusCode.OK, courses);
        }

        [Route("")]
        public async Task<HttpResponseMessage> Post(CourseCreateItem course)
        {
            await this.repositoryService.Save(course);
            return this.Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("{id:int}")]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            await this.repositoryService.Delete(id);
            return this.Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
