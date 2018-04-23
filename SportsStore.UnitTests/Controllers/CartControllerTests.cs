using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Interfaces;
using SportsStore.UnitTests.Helpers;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.Models;
using System.Linq;

namespace SportsStore.UnitTests.Controllers
{
    [TestClass]
    public class CartControllerTests
    {
        private Mock<IProductService> productService;
        private Mock<IOrderService> orderService;
        private Product product;

        [TestInitialize]
        public void Test_Initialize()
        {
            productService = new Mock<IProductService>();
            orderService = new Mock<IOrderService>();
            product = new Product { ProductId = 1, Name = "P1", CategoryId = 1 };
        }

        [TestMethod]
        public void AddToCart_FetchesProductFromService()
        {
            var cart = new Cart();
            productService.Setup(ps => ps.GetProduct(It.IsAny<int>())).Returns(product);
            var controller = new CartController(productService.Object, orderService.Object);

            controller.AddToCart(cart, 1, null);

            productService.Verify(ps => ps.GetProduct(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public void AddToCart_AddsCorrectAmountOfProducts()
        {
            var cart = new Cart();
            productService.Setup(ps => ps.GetProduct(It.IsAny<int>())).Returns(product);
            var controller = new CartController(productService.Object, orderService.Object);

            controller.AddToCart(cart, 1, null);

            Assert.AreEqual(cart.Items.Count, 1);
        }

        [TestMethod]
        public void AddToCart_AddsProperProduct()
        {
            var cart = new Cart();
            productService.Setup(ps => ps.GetProduct(It.IsAny<int>())).Returns(product);
            var controller = new CartController(productService.Object, orderService.Object);

            controller.AddToCart(cart, 1, null);

            productService.Verify(ps => ps.GetProduct(It.IsAny<int>()), Times.Once);
            Assert.AreEqual(cart.Items.Count, 1);
            Assert.AreEqual(cart.Items.ElementAt(0).Product.ProductId, 1);
        }

        [TestMethod]
        public void AddToCart_RedirectsToCart()
        {
            const string returnUrl = "TestUrl";
            var cart = new Cart();
            productService.Setup(ps => ps.GetProduct(It.IsAny<int>())).Returns(product);
            var controller = new CartController(productService.Object, orderService.Object);

            var result = controller.AddToCart(cart, 1, returnUrl);

            Assert.AreEqual(result.RouteValues["action"], "Index");
            Assert.AreEqual(result.RouteValues["returnUrl"], returnUrl);
        }

        [TestMethod]
        public void Index_CanViewCartContents()
        {
            const string returnUrl = "TestUrl";
            var cart = new Cart();
            var controller = new CartController(productService.Object, orderService.Object);

            var result = (CartIndexViewModel)controller.Index(cart, returnUrl).ViewData.Model;

            CollectionAssert.AreEqual(result.Items.ToList(), cart.Items.ToList());
            Assert.AreEqual(result.ReturnUrl, returnUrl);
        }

        [TestMethod]
        public async void Checkout_Empty_Cart_ModelState_Has_Errors()
        {
            var cart = new Cart();
            var shippingInformationViewModel = new ShippingInformationViewModel();
            var controller = new CartController(productService.Object, orderService.Object);

            var result = await controller.Checkout(cart, shippingInformationViewModel);

            orderService.Verify(s => s.AddOrder(It.IsAny<Order>()), Times.Never);
            Assert.AreEqual(string.Empty, result.ViewName);
            Assert.IsFalse(result.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public async void Checkout_Invalid_ModelState_Does_Nothing()
        {
            var cart = TestDataProvider.Cart;
            var shippingInformationViewModel = new ShippingInformationViewModel();
            var controller = new CartController(productService.Object, orderService.Object);
            controller.ModelState.AddModelError("error", "error");

            var result = await controller.Checkout(cart, shippingInformationViewModel);

            orderService.Verify(s => s.AddOrder(It.IsAny<Order>()), Times.Never);
            Assert.AreEqual(string.Empty, result.ViewName);
            Assert.IsFalse(result.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public async void Checkout_Valid_Cart_And_Shipping_Information_Calls_Order_Service()
        {
            var cart = TestDataProvider.Cart;
            var shippingInformationViewModel = new ShippingInformationViewModel();
            var controller = new CartController(productService.Object, orderService.Object);

            var result = await controller.Checkout(cart, shippingInformationViewModel);

            orderService.Verify(s => s.AddOrder(It.IsAny<Order>()), Times.Once);
            Assert.AreEqual("Completed", result.ViewName);
            Assert.IsTrue(result.ViewData.ModelState.IsValid);
        }
    }
}