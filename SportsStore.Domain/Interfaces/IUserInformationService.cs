using SportsStore.Domain.Entities;
using System.Threading.Tasks;

namespace SportsStore.Domain.Interfaces
{
    public interface IUserInformationService
    {
        UserInformation GetUserInformation(int userId);

        Task CreateUserInformation(UserInformation userInformation);

        Task EditUserInformation(UserInformation updatedUserInformation);
    }
}