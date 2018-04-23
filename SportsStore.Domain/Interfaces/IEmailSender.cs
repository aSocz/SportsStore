using SportsStore.Domain.Entities;
using System.Threading.Tasks;

namespace SportsStore.Domain.Interfaces
{
    public interface IEmailSender
    {
        Task Send(Email email);
    }
}