using SportsStore.Domain.Entities;
using System.Threading.Tasks;

namespace SportsStore.Domain.Interfaces
{
    public interface IOrderService
    {
        Task AddOrder(Order order);
    }
}