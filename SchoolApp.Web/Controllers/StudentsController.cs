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

    using DataAccess.DomainObjects;

    [RoutePrefix("api/students")]
    public class StudentsController : ApiController
    {
        private readonly IRepository<Student> repository;

        public StudentsController(IRepository<Student> repository)
        {
            this.repository = repository;
        }

        [Route("")]
        public async Task<HttpResponseMessage> Get(PageableListQuery query)
        {
            query = query ?? new PageableListQuery();
            var students = await this.repository.GetPagedList(query);
            var list = Mapper.Map<IEnumerable<Student>, IEnumerable<StudentItem>>(students.List);

            return this.Request.CreateResponse(
                HttpStatusCode.OK, new PagedListResult<StudentItem>(list, students.Count));
        }

        [Route("{id:int}")]
        public async Task<HttpResponseMessage> Get(int id)
        {
            var student = await this.repository.Get(id);
            var srudentItem = Mapper.Map<StudentItem>(student);
            return this.Request.CreateResponse(HttpStatusCode.OK, srudentItem);
        }

        [Route("")]
        public async Task<HttpResponseMessage> Post(StudentCreateItem studentItem)
        {
            var student = Mapper.Map<Student>(studentItem);
            await this.repository.Save(student);
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
