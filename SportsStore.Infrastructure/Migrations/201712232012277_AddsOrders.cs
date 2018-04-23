namespace SportsStore.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddsOrders : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "SportsStore.CartLines",
                c => new
                    {
                        CartLineId = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        CartId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CartLineId)
                .ForeignKey("SportsStore.Carts", t => t.CartId, cascadeDelete: true)
                .ForeignKey("SportsStore.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.CartId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "SportsStore.Carts",
                c => new
                    {
                        CartId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.CartId);
            
            CreateTable(
                "SportsStore.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        ClientName = c.String(maxLength: 50),
                        GiftWrap = c.Boolean(nullable: false),
                        AddressLine1 = c.String(maxLength: 20),
                        AddressLine2 = c.String(maxLength: 20),
                        AddressLine3 = c.String(maxLength: 20),
                        AddressCity = c.String(maxLength: 20),
                        AddressState = c.String(maxLength: 20),
                        AddressZip = c.String(maxLength: 10),
                        AddressCountry = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("SportsStore.Carts", t => t.OrderId)
                .Index(t => t.OrderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("SportsStore.Orders", "OrderId", "SportsStore.Carts");
            DropForeignKey("SportsStore.CartLines", "ProductId", "SportsStore.Products");
            DropForeignKey("SportsStore.CartLines", "OrderId", "SportsStore.Carts");
            DropIndex("SportsStore.Orders", new[] { "OrderId" });
            DropIndex("SportsStore.CartLines", new[] { "ProductId" });
            DropIndex("SportsStore.CartLines", new[] { "OrderId" });
            DropTable("SportsStore.Orders");
            DropTable("SportsStore.Carts");
            DropTable("SportsStore.CartLines");
        }
    }
}
