﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Interfaces;
using SportsStore.UnitTests.Helpers;
using SportsStore.WebUI.Areas.Admin.Controllers;
using System.Collections.Generic;
using System.Linq;

namespace SportsStore.UnitTests.Controllers
{
    [TestClass]
    public class Admin_ProductsTests
    {
        private Mock<IProductService> productService;
        private Mock<ICategoryService> categoryService;
        private List<Product> products;

        [TestInitialize]
        public void Test_Initialize()
        {
            productService = new Mock<IProductService>();
            categoryService = new Mock<ICategoryService>();
            products = TestDataProvider.Products.ToList();
        }

        [TestMethod]
        public void Index_ReturnsAllProducts()
        {
            productService.Setup(ps => ps.GetAllProducts()).Returns(products);
            var controller = new ProductsController(productService.Object, categoryService.Object);

            var result = ((IEnumerable<Product>)controller.Index().ViewData.Model).ToList();

            Assert.AreEqual(products.Count, result.Count);
            for (var i = 0; i < products.Count; i++)
            {
                Assert.AreEqual(products[i].Name, result[i]?.Name);
            }
        }
    }
}