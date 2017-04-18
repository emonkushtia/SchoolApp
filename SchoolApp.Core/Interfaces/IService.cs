namespace SchoolApp.Core.Interfaces
{
    using System.Threading.Tasks;

    using Infastucture;

    public interface IRepositoryService<T1, in T2> where T1 : class where T2 : class 
    {
        Task<PagedListResult<T1>> GetPagedList(PageableListQuery query);

        Task<T1> Get(int id);

        Task Save(T2 obj);

        Task Update(T2 obj);

        Task Delete(int id);
    }
}
