namespace SchoolApp.Web.Controllers
{
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;

    using Core.DataTransferObjects;
    using Core.Infastucture;
    using Core.Interfaces;

    [RoutePrefix("api/students")]
    public class StudentsController : ApiController
    {
        private readonly IRepositoryService<StudentItem, StudentCreateItem> repositoryService;

        public StudentsController(IRepositoryService<StudentItem, StudentCreateItem> repositoryService)
        {
            this.repositoryService = repositoryService;
        }

        [Route("")]
        public async Task<HttpResponseMessage> Get(PageableListQuery query)
        {
            query = query ?? new PageableListQuery();
            var students = await this.repositoryService.GetPagedList(query);
            return this.Request.CreateResponse(
                HttpStatusCode.OK, students);
        }

        [Route("{id:int}")]
        public async Task<HttpResponseMessage> Get(int id)
        {
            var student = await this.repositoryService.Get(id);
            return this.Request.CreateResponse(HttpStatusCode.OK, student);
        }

        [Route("")]
        public async Task<HttpResponseMessage> Post(StudentCreateItem student)
        {
            await this.repositoryService.Save(student);
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
