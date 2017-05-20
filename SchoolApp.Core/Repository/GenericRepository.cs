namespace SchoolApp.Core.Repository
{
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Dynamic;
    using System.Threading.Tasks;

    using DataAccess;

    using Infastucture;
    using Interfaces;

    using SeliseSchool.DataAccess.DomainObjects;

    public class GenericRepository<TEntity> : IRepository<TEntity>
        where TEntity : DomainModel
    {
        private readonly DataContext context;

        private readonly DbSet<TEntity> dbSet;

        public GenericRepository(DataContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public async Task<PagedListResult<TEntity>> GetPagedList(PageableListQuery query)
        {
            var entities = await this.dbSet

                    // .Where("name.Contains(@0)", "Em")
                    .OrderBy("name asc")
                    .Skip(query.Offset.GetValueOrDefault())
                    .Take(query.Limit)
                    .ToListAsync();

            var count = await this.dbSet.CountAsync();
            return new PagedListResult<TEntity>(entities, count);
        }

        public async Task<TEntity> Get(int id)
        {
            return await this.dbSet.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Save(TEntity entity)
        {
            if (entity.Id == default(int))
            {
                this.dbSet.Add(entity);
            }

            await this.context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await this.dbSet.FirstOrDefaultAsync(x => x.Id == id);
            if (entity != null)
            {
                this.dbSet.Remove(entity);
                await this.context.SaveChangesAsync();
            }
        }
    }
}
