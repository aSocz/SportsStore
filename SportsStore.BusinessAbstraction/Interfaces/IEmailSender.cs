using SportsStore.Domain.Entities;
using System.Threading.Tasks;

namespace SportsStore.BusinessAbstraction.Interfaces
{
    public interface IEmailSender
    {
        Task Send(Email email);
    }
}