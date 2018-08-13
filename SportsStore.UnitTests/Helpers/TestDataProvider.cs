using Moq;
using SportsStore.Domain.Entities;
using SportsStore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace SportsStore.UnitTests.Helpers
{
    public class TestDataProvider
    {
        public static readonly Address Address = new Address("Line1", "City", "State", "Zip", "Country", "Line2", "Line3");
        public static readonly Product[] Products =
        {
            new Product {ProductId = 1, CategoryId = 1, Name = "P1", IsActive = true},
            new Product {ProductId = 2, CategoryId = 1, Name = "P2", IsActive = true},
            new Product {ProductId = 3, CategoryId = 1, Name = "P3", IsActive = true},
            new Product {ProductId = 4, CategoryId = 2, Name = "P4", IsActive = true},
            new Product {ProductId = 5, CategoryId = 2, Name = "P5", IsActive = true},
            new Product {ProductId = 6, CategoryId = 3, Name = "P6", IsActive = true},
            new Product {ProductId = 7, CategoryId = 3, Name = "P7", IsActive = true}
        };

        public static readonly Category[] Categories =
        {
            new Category {CategoryId = 1, Name = "C1"},
            new Category {CategoryId = 2, Name = "C2"},
            new Category {CategoryId = 3, Name = "C3"},
            new Category {CategoryId = 4, Name = "C4"},
            new Category {CategoryId = 5, Name = "C5"},
            new Category {CategoryId = 6, Name = "C6"}
        };

        public static readonly Cart Cart;
        public static Order Order;

        static TestDataProvider()
        {
            Cart = new Cart();
            FillData();
        }

        public static Mock<FakeUnitOfWork> GetFakeUnitOfWorkMock()
        {
            return new Mock<FakeUnitOfWork>();
        }

        public static Mock<SportsStoreContext> GetDbContextMock()
        {
            var dbContextMock = new Mock<SportsStoreContext>();
            var products = CreateDbSetMock(Products).Object;
            var categories = CreateDbSetMock(Categories).Object;
            dbContextMock.Setup(c => c.Products).Returns(products);
            dbContextMock.Setup(c => c.Set<Product>()).Returns(products);
            dbContextMock.Setup(c => c.Categories).Returns(categories);
            dbContextMock.Setup(c => c.Set<Category>()).Returns(categories);

            return dbContextMock;
        }

        private static Mock<DbSet<T>> CreateDbSetMock<T>(IEnumerable<T> elements) where T : class
        {
            var elementsAsQueryable = elements.AsQueryable();
            var dbSetMock = new Mock<DbSet<T>>();

            dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(elementsAsQueryable.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(elementsAsQueryable.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(elementsAsQueryable.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(elementsAsQueryable.GetEnumerator());
            dbSetMock.Setup(s => s.AsNoTracking()).Returns(dbSetMock.Object);

            return dbSetMock;
        }

        private static void FillData()
        {
            Cart.AddItem(Products[0], 10);
            Cart.AddItem(Products[1], 20);
            Cart.AddItem(Products[2], 30);
            Cart.AddItem(Products[3], 40);
            Cart.AddItem(Products[4], 50);
            Cart.AddItem(Products[5], 60);
            Cart.AddItem(Products[6], 70);
            Order = new Order
            {
                Address = Address,
                CartLines = Cart.Items.ToList(),
                ClientEmail = "testEmail@test.com",
                ClientName = "Test",
                GiftWrap = true,
                OrderDate = DateTime.UtcNow
            };
        }
    }
}