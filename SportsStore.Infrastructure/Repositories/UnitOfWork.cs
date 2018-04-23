using SportsStore.Domain.Interfaces;
using System.Threading.Tasks;

namespace SportsStore.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SportsStoreContext dbContext;

        public UnitOfWork(SportsStoreContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }

        public IRepository<T> GetRepository<T>() where T : class => new Repository<T>(dbContext);
    }
}