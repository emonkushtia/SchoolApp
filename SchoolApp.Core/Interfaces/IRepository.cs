namespace SchoolApp.Core.Interfaces
{
    using System.Threading.Tasks;

    using Infastucture;

    public interface IRepository<T> where T : class
    {
        Task<PagedListResult<T>> GetPagedList(PageableListQuery query);

        Task<T> Get(int id);

        Task Save(T model);

        Task Delete(int id);
    }
}
