using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Business.Services;
using SportsStore.Domain.Interfaces;
using SportsStore.UnitTests.Helpers;
using System.Linq;

namespace SportsStore.UnitTests.Services
{
    [TestClass]
    public class ProductServiceTests
    {
        private Mock<FakeUnitOfWork> unitOfWork;
        private IProductService productService;
        private const int PageSize = 2;
        private const int TestCategoryId = 1;

        [TestInitialize]
        public void Test_Initialize()
        {
            unitOfWork = TestDataProvider.GetFakeUnitOfWorkMock();
        }

        [TestMethod]
        public void GetPagedProducts_SecondPage_ReturnsProduct3And4()
        {
            productService = new ProductService(unitOfWork.Object);

            var result = productService.GetPagedProducts(2, PageSize).ToArray();

            Assert.IsTrue(result.Length == 2);
            Assert.AreEqual("P3", result[0].Name);
            Assert.AreEqual("P4", result[1].Name);
        }

        [TestMethod]
        public void GetPagedProducts_LastPage_ReturnsProduct7()
        {
            productService = new ProductService(unitOfWork.Object);

            var result = productService.GetPagedProducts(4, PageSize).ToArray();

            Assert.IsTrue(result.Length == 1);
            Assert.AreEqual("P7", result[0].Name);
        }

        [TestMethod]
        public void GetPagedProducts_NotExistingPage_DoesntReturnProducts()
        {
            productService = new ProductService(unitOfWork.Object);

            var result = productService.GetPagedProducts(10, PageSize).ToArray();

            Assert.IsFalse(result.Any());
        }

        [TestMethod]
        public void GetCategoryPagedProducts_SecondPage_ReturnsProduct3()
        {
            productService = new ProductService(unitOfWork.Object);

            var result = productService.GetCategoryPagedProducts(1, 2, PageSize).ToArray();

            Assert.IsTrue(result.Length == 1);
            Assert.AreEqual("P3", result[0].Name);
        }

        [TestMethod]
        public void GetCategoryPagedProducts_LastPage_ReturnsProduct3()
        {
            productService = new ProductService(unitOfWork.Object);

            var result = productService.GetCategoryPagedProducts(TestCategoryId, 2, PageSize).ToArray();

            Assert.IsTrue(result.Length == 1);
            Assert.AreEqual("P3", result[0].Name);
        }

        [TestMethod]
        public void GetCategoryPagedProducts_NotExistingPage_DoesntReturnProducts()
        {
            productService = new ProductService(unitOfWork.Object);

            var result = productService.GetCategoryPagedProducts(TestCategoryId, 10, PageSize).ToArray();

            Assert.IsFalse(result.Any());
        }

        [TestMethod]
        public void GetCategoryPagedProducts_NotExistingCategory_DoesntReturnProducts()
        {
            productService = new ProductService(unitOfWork.Object);

            var result = productService.GetCategoryPagedProducts(10, 1, PageSize).ToArray();

            Assert.IsFalse(result.Any());
        }

        [TestMethod]
        public void GetProductsCount_NoCategory_ReturnsCountOfAllProducts()
        {
            productService = new ProductService(unitOfWork.Object);

            var result = productService.GetProductsCount();

            Assert.IsTrue(result == TestDataProvider.Products.Length);
        }

        [TestMethod]
        public void GetProductsCount_ExistingCategory_ReturnsCountOfProductsInCategory()
        {
            productService = new ProductService(unitOfWork.Object);

            var result = productService.GetProductsCount(1);

            Assert.IsTrue(result == TestDataProvider.Products.Count(p => p.CategoryId == 1));
        }

        [TestMethod]
        public void GetProductsCount_NotExistingCategory_ReturnsZero()
        {
            productService = new ProductService(unitOfWork.Object);

            var result = productService.GetProductsCount(10);

            Assert.IsTrue(result == TestDataProvider.Products.Count(p => p.CategoryId == 10));
        }

        [TestMethod]
        public void GetProduct_NotExistingProduct_ReturnsNull()
        {
            productService = new ProductService(unitOfWork.Object);

            var result = productService.GetProduct(8);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetProduct_ExistingProduct_ReturnsProduct()
        {
            productService = new ProductService(unitOfWork.Object);

            var result = productService.GetProduct(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.ProductId, 1);
        }
    }
}
