using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Interfaces;
using System.Linq;

namespace SportsStore.UnitTests.Domain
{
    [TestClass]
    public class CartTests
    {
        private IShoppingCart _cart;
        private Product _testProduct;

        [TestInitialize]
        public void Test_Initialize()
        {
            _cart = new Cart();
            _testProduct = new Product
            {
                ProductId = 1,
                CategoryId = 1,
                Description = "test product",
                Name = "test product",
                Price = 17
            };
        }

        [TestMethod]
        public void AddItem_EmptyCart_AddsItemToCart()
        {
            _cart.AddItem(_testProduct, 5);
            Assert.AreEqual(_cart.Items.Count, 1);
            Assert.AreEqual(_cart.Items.First().Product.ProductId, _testProduct.ProductId);
        }

        [TestMethod]
        public void AddItem_EmptyCart_AddsProperProductQuantity()
        {
            _cart.AddItem(_testProduct, 5);
            Assert.AreEqual(_cart.Items.First().Quantity, 5);
        }

        [TestMethod]
        public void AddItem_NullProduct_DoesNothing()
        {
            _cart.AddItem(null, 5);
            Assert.AreEqual(_cart.Items.Count, 0);
        }

        [TestMethod]
        public void AddItem_CartWithProduct_IncreasesProductQuantity()
        {
            _cart.AddItem(_testProduct, 10);

            _cart.AddItem(_testProduct, 5);
            Assert.AreEqual(_cart.Items.Count, 1);
            Assert.AreEqual(_cart.Items.First().Quantity, 15);
        }

        [TestMethod]
        public void Clear_CartWithProduct_ClearsCart()
        {
            _cart.AddItem(_testProduct, 10);

            _cart.Clear();
            Assert.AreEqual(_cart.Items.Count, 0);
        }

        [TestMethod]
        public void Clear_EmptyCart_ClearsCart()
        {
            _cart.Clear();
            Assert.AreEqual(_cart.Items.Count, 0);
        }

        [TestMethod]
        public void RemoveItem_EmptyCart_DoesNothing()
        {
            _cart.RemoveItem(_testProduct);
            Assert.AreEqual(_cart.Items.Count, 0);
        }

        [TestMethod]
        public void RemoveItem_ExistingItem_RemovesItem()
        {
            _cart.AddItem(_testProduct, 10);

            _cart.RemoveItem(_testProduct);
            Assert.AreEqual(_cart.Items.Count, 0);
        }

        [TestMethod]
        public void RemoveItem_NullProduct_DoesNothing()
        {
            _cart.AddItem(_testProduct, 10);
            _cart.RemoveItem(null);
            Assert.AreEqual(_cart.Items.Count, 1);
        }

        [TestMethod]
        public void ComputeTotal_ExistingItems_ComputesValidTotal()
        {
            _cart.AddItem(_testProduct, 10);

            var result = _cart.ComputeTotal();
            Assert.AreEqual(result, 170);
        }
    }
}
