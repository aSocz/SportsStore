using SportsStore.Domain.Entities;
using System.Data.Entity;

namespace SportsStore.Infrastructure
{
    public class SportsStoreContext : DbContext
    {
        public SportsStoreContext() : base("name=SportsStore")
        {
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<CartLine> CartLines { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("SportsStore");
            SetupProductTable(modelBuilder);
            SetupCategoryTable(modelBuilder);
            SetupOrderTable(modelBuilder);
            SetupCartLineTable(modelBuilder);
        }

        private static void SetupCartLineTable(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartLine>().HasKey(cl => cl.CartLineId);
            modelBuilder.Entity<CartLine>()
                        .HasRequired(cl => cl.Order)
                        .WithMany(o => o.CartLines)
                        .HasForeignKey(cl => cl.OrderId);
            modelBuilder.Entity<CartLine>()
                        .HasRequired(cl => cl.Product)
                        .WithMany(p => p.CartLines)
                        .HasForeignKey(cl => cl.ProductId);
        }

        private static void SetupOrderTable(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().HasKey(o => o.OrderId);
            modelBuilder.Entity<Order>().Property(o => o.ClientName).HasMaxLength(50);
            modelBuilder.Entity<Order>()
                        .Property(o => o.Address.Line1)
                        .HasColumnName("AddressLine1")
                        .HasMaxLength(20)
                        .IsRequired();
            modelBuilder.Entity<Order>().Property(o => o.Address.Line2).HasColumnName("AddressLine2").HasMaxLength(20);
            modelBuilder.Entity<Order>().Property(o => o.Address.Line3).HasColumnName("AddressLine3").HasMaxLength(20);
            modelBuilder.Entity<Order>()
                        .Property(o => o.Address.City)
                        .HasColumnName("AddressCity")
                        .HasMaxLength(20)
                        .IsRequired();
            modelBuilder.Entity<Order>()
                        .Property(o => o.Address.State)
                        .HasColumnName("AddressState")
                        .HasMaxLength(20)
                        .IsRequired();
            modelBuilder.Entity<Order>()
                        .Property(o => o.Address.Zip)
                        .HasColumnName("AddressZip")
                        .HasMaxLength(10)
                        .IsRequired();
            modelBuilder.Entity<Order>()
                        .Property(o => o.Address.Country)
                        .HasColumnName("AddressCountry")
                        .HasMaxLength(20)
                        .IsRequired();
            modelBuilder.Entity<Order>().Property(o => o.ClientEmail).HasMaxLength(50);
            modelBuilder.Entity<Order>().Property(o => o.OrderDate).IsRequired();
        }

        private static void SetupCategoryTable(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasKey(e => e.CategoryId);
            modelBuilder.Entity<Category>().Property(e => e.Name).IsRequired().HasMaxLength(50);
        }

        private static void SetupProductTable(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                        .HasKey(e => e.ProductId)
                        .HasRequired(e => e.Category)
                        .WithMany(e => e.Products)
                        .HasForeignKey(e => e.CategoryId);
            modelBuilder.Entity<Product>().Property(e => e.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Product>().Property(e => e.Price).HasPrecision(10, 2).IsRequired();
            modelBuilder.Entity<Product>().Property(e => e.Thumbnail.ImageData).HasColumnName("ImageData");
            modelBuilder.Entity<Product>().Property(e => e.Thumbnail.ImageType).HasColumnName("ImageMimeType");
        }
    }
}