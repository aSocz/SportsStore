using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Business.Services;
using SportsStore.Domain.Interfaces;
using SportsStore.UnitTests.Helpers;
using System.Linq;

namespace SportsStore.UnitTests.Services
{
    [TestClass]
    public class CategoryServiceTests
    {
        private Mock<FakeUnitOfWork> unitOfWork;
        private ICategoryService categoryService;

        [TestInitialize]
        public void Test_Initialize()
        {
            unitOfWork = TestDataProvider.GetFakeUnitOfWorkMock();
            categoryService = new CategoryService(unitOfWork.Object);
        }

        [TestMethod]
        public void GetCategories_ReturnsAllCategories()
        {
            var result = categoryService.GetCategories();

            Assert.AreEqual(TestDataProvider.Categories.Length, result.Count());
        }
    }
}