using System.Threading.Tasks;

namespace SportsStore.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
        IRepository<T> GetRepository<T>() where T : class;
    }
}