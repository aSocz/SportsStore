using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Interfaces;
using SportsStore.WebUI.Controllers;
using System.Web.Mvc;

namespace SportsStore.UnitTests.Controllers
{
    [TestClass]
    public class ImageTests
    {
        [TestMethod]
        public void Can_Retrieve_Image_Data()
        {
            var product = new Product
            {
                ProductId = 2,
                Name = "Test",
                Thumbnail = new Image(new byte[] { }, "image/png")
            };

            var productService = new Mock<IProductService>();
            productService.Setup(ps => ps.GetProduct(product.ProductId)).Returns(product);
            productService.Setup(ps => ps.GetProduct(It.IsNotIn(product.ProductId))).Returns(new Product { ProductId = 1, Name = "P1" });

            var controller = new ProductsController(productService.Object);

            var result = controller.GetImage(product.ProductId);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(FileResult));
            Assert.AreEqual(product.Thumbnail.ImageType, (result as FileResult).ContentType);
        }

        [TestMethod]
        public void Cannot_Retrieve_Image_Data_For_Invalid_Id()
        {
            var product = new Product
            {
                ProductId = 2,
                Name = "Test",
                Thumbnail = new Image(new byte[] { }, "image/png")
            };

            var productService = new Mock<IProductService>();
            productService.Setup(ps => ps.GetProduct(product.ProductId)).Returns(product);
            productService.Setup(ps => ps.GetProduct(It.IsNotIn(product.ProductId))).Returns(new Product { ProductId = 1, Name = "P1" });

            var controller = new ProductsController(productService.Object);

            var result = controller.GetImage(100);

            Assert.IsNull(result);
        }
    }
}