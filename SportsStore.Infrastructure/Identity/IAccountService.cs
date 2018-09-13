using SportsStore.Business.Models;
using SportsStore.Business.Validation;
using System.Threading.Tasks;

namespace SportsStore.Business.Interfaces
{
    public interface IAccountService
    {
        Task<ValidationResult> Login(string userName, string password);
        Task<ValidationResult> Create(AccountDto accountDto);
        Task<ValidationResult> Update(int userId, AccountDto accountDto);
    }
}