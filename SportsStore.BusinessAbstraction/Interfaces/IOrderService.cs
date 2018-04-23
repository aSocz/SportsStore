using SportsStore.Domain.Entities;
using System.Threading.Tasks;

namespace SportsStore.BusinessAbstraction.Interfaces
{
    public interface IOrderService
    {
        Task AddOrder(Order order);
    }
}