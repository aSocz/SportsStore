using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Business.Services;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Interfaces;
using SportsStore.UnitTests.Helpers;

namespace SportsStore.UnitTests.Services
{
    [TestClass]
    public class OrderServiceTests
    {
        private IOrderService orderService;
        private Mock<FakeUnitOfWork> unitOfWork;
        private Mock<IEmailSender> emailSender;

        [TestInitialize]
        public void Test_Initialize()
        {
            unitOfWork = TestDataProvider.GetFakeUnitOfWorkMock();
            emailSender = new Mock<IEmailSender>();
            orderService = new OrderService(emailSender.Object, unitOfWork.Object);
        }

        [TestMethod]
        public async void Add_Order_Adds_Order_To_Database()
        {
            await orderService.AddOrder(TestDataProvider.Order);

            unitOfWork.Verify(uow => uow.GetRepository<Order>().Insert(TestDataProvider.Order), Times.Once);
            unitOfWork.Verify(uow => uow.SaveChangesAsync(), Times.Once);
        }

        [TestMethod]
        public async void Add_Order_Sends_An_Email()
        {
            await orderService.AddOrder(TestDataProvider.Order);

            unitOfWork.Verify(uow => uow.GetRepository<Order>().Insert(TestDataProvider.Order), Times.Once);
            unitOfWork.Verify(uow => uow.SaveChangesAsync(), Times.Once);

            emailSender.Verify(es => es.Send(It.IsAny<Email>()));
        }
    }
}