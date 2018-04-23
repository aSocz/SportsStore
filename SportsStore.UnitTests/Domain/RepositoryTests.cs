using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Interfaces;
using SportsStore.Infrastructure;
using SportsStore.Infrastructure.Repositories;
using SportsStore.UnitTests.Helpers;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SportsStore.UnitTests.Domain
{
    [TestClass]
    public class RepositoryTests
    {
        private IRepository<Product> repository;
        private Mock<SportsStoreContext> dbContext;

        [TestInitialize]
        public void Test_Initialize()
        {
            dbContext = TestDataProvider.GetDbContextMock();
            repository = new Repository<Product>(dbContext.Object);
        }

        [TestMethod]
        public void Get_Returns_Valid_Data()
        {
            Expression<Func<Product, bool>> predicate = product => true;
            var result = repository.Get(predicate).ToList();
            var expectedResult = dbContext.Object.Products.Where(predicate).ToList();

            CollectionAssert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void GetAll_Returns_Valid_Data()
        {
            var result = repository.GetAll().ToList();
            var expectedResult = dbContext.Object.Products.Where(p => true).ToList();

            CollectionAssert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void GetNoTracking_Returns_Valid_Data()
        {
            Expression<Func<Product, bool>> predicate = product => true;
            var result = repository.GetNoTracking(predicate).ToList();
            var expectedResult = dbContext.Object.Products.Where(predicate).ToList();

            CollectionAssert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void GetAllNoTracking_Returns_Valid_Data()
        {
            var result = repository.GetAllNoTracking().ToList();
            var expectedResult = dbContext.Object.Products.Where(p => true).ToList();

            CollectionAssert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void Insert_Adds_Entity_To_DbSet()
        {
            var product = TestDataProvider.Products.First();
            repository.Insert(product);

            MoqHelper.GetMockFromObject(dbContext.Object.Products).Verify(p => p.Add(product), Times.Once);
        }
    }
}