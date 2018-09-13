using Microsoft.AspNet.Identity;
using SportsStore.Domain.Constants;
using SportsStore.Domain.Entities;
using SportsStore.Infrastructure.Identity;
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
            var userManager = new SportsStoreUserManager(new UserStore(context));
            var roleManager = new RoleManager<Role, int>(new RoleStore(context));
            if (!roleManager.RoleExists(Roles.Admin))
            {
                roleManager.Create(new Role(Roles.Admin));
            }

            if (!roleManager.RoleExists(Roles.Customer))
            {
                roleManager.Create(new Role(Roles.Customer));
            }

            var user = userManager.FindByName("Admin");
            if (user == null)
            {
                var adminAddress = new Address("line1", "adminCity", "adminState", "adminZip", "adminCountry");
                var adminInformation = new UserInformation(adminAddress);
                var admin = new SportsStoreUser("Admin", "admin@gmail.com", adminInformation)
                {
                    PasswordHash = userManager.PasswordHasher.HashPassword("admin123")
                };

                userManager.Create(admin);
                user = userManager.FindByName("Admin");
            }

            if (!userManager.IsInRole(user.Id, Roles.Admin))
            {
                userManager.AddToRole(user.Id, Roles.Admin);
            }

            if (!userManager.IsInRole(user.Id, Roles.Customer))
            {
                userManager.AddToRole(user.Id, Roles.Customer);
            }

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
