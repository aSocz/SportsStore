using SportsStore.Domain.Constants;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Interfaces;
using System.Threading.Tasks;

namespace SportsStore.Business.Services
{
    public class OrderService : IOrderService
    {
        private readonly IEmailSender emailSender;
        private readonly IUnitOfWork unitOfWork;

        public OrderService(IEmailSender emailSender, IUnitOfWork unitOfWork)
        {
            this.emailSender = emailSender;
            this.unitOfWork = unitOfWork;
        }

        public async Task AddOrder(Order order)
        {
            unitOfWork.GetRepository<Order>().Insert(order);
            await unitOfWork.SaveChangesAsync().ConfigureAwait(false);

            if (string.IsNullOrWhiteSpace(order.ClientEmail))
            {
                return;
            }

            var email = new Email(order.GetOrderSummary(), Constants.OrderEmailSubject, order.ClientEmail);
            await email.Send(emailSender).ConfigureAwait(false);
        }
    }
}