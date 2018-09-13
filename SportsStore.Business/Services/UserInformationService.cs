using SportsStore.Business.Extensions;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Business.Services
{
    public class UserInformationService : IUserInformationService
    {
        private readonly IRepository<UserInformation> userInformationRepository;
        private readonly IUnitOfWork unitOfWork;

        public UserInformationService(IUnitOfWork unitOfWork)
        {
            userInformationRepository = unitOfWork.GetRepository<UserInformation>();
            this.unitOfWork = unitOfWork;
        }

        public UserInformation GetUserInformation(int userId)
        {
            return userInformationRepository.Get(ui => ui.UserInformationId == userId).FirstOrDefault();
        }

        public async Task CreateUserInformation(UserInformation userInformation)
        {
            userInformation.ThrowIfNull();
            userInformationRepository.Insert(userInformation);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task EditUserInformation(UserInformation updatedUserInformation)
        {
            updatedUserInformation.ThrowIfNull();
            var userInformation = userInformationRepository
                         .Get(ui => ui.UserInformationId == updatedUserInformation.UserInformationId)
                         .FirstOrDefault();
            userInformation.ThrowIfNull();

            userInformation.Update(updatedUserInformation);
            await unitOfWork.SaveChangesAsync();
        }
    }
}