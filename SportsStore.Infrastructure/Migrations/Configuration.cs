using SportsStore.Domain.Entities;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace SportsStore.Infrastructure.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<SportsStoreContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SportsStoreContext context)
        {
            if (!context.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category {Name = "Sporty wodne"},
                    new Category {Name = "Pi³ka no¿na"},
                    new Category {Name = "Rowery"}
                };

                context.Categories.AddRange(categories);
            }

            context.SaveChanges();
            if (!context.Products.Any() && context.Categories.Any())
            {
                var categories = context.Categories.ToList();
                var products = new List<Product>
                {
                    new Product
                    {
                        Name = "Kajak",
                        Description = "Opis kajaku",
                        Price = 275m,
                        CategoryId = categories.First(c => c.Name.Equals("Sporty wodne")).CategoryId
                    },
                    new Product
                    {
                        Name = "Kamizelka ratunkowa",
                        Description = "Opis kamizelki ratunkowej",
                        Price = 275m,
                        CategoryId = categories.First(c => c.Name.Equals("Sporty wodne")).CategoryId
                    },
                    new Product
                    {
                        Name = "Pi³ka",
                        Description = "Rozmiar 5",
                        Price = 19.5m,
                        CategoryId = categories.First(c => c.Name.Equals("Pi³ka no¿na")).CategoryId
                    },
                    new Product
                    {
                        Name = "Strój pi³karski",
                        Description = "Rozmiar L",
                        Price = 34.95m,
                        CategoryId = categories.First(c => c.Name.Equals("Pi³ka no¿na")).CategoryId
                    },
                    new Product
                    {
                        Name = "Rower MTB",
                        Description = "Opis",
                        Price = 1600m,
                        CategoryId = categories.First(c => c.Name.Equals("Rowery")).CategoryId
                    },
                    new Product
                    {
                        Name = "Rower szosowy",
                        Description = "Opis",
                        Price = 2999.95m,
                        CategoryId = categories.First(c => c.Name.Equals("Rowery")).CategoryId
                    },
                    new Product
                    {
                        Name = "Peda³y SPD-SL",
                        Description = "Peda³y szosowe",
                        Price = 250m,
                        CategoryId = categories.First(c => c.Name.Equals("Rowery")).CategoryId
                    }
                };

                context.Products.AddRange(products);
                context.SaveChanges();
            }
        }
    }
}
