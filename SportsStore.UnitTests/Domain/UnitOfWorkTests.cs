using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Infrastructure;
using SportsStore.Infrastructure.Repositories;

namespace SportsStore.UnitTests.Domain
{
    [TestClass]
    public class UnitOfWorkTests
    {
        [TestMethod]
        public async void SaveChangesAsync_Calls_ORM_Saving()
        {
            var dbContext = new Mock<SportsStoreContext>();
            var unitOfWork = new UnitOfWork(dbContext.Object);

            await unitOfWork.SaveChangesAsync();

            dbContext.Verify(c => c.SaveChangesAsync(), Times.Once);
        }
    }
}