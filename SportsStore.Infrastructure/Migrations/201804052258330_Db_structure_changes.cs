namespace SportsStore.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Db_structure_changes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("SportsStore.CartLines", "CartId", "SportsStore.Carts");
            DropForeignKey("SportsStore.Orders", "OrderId", "SportsStore.Carts");
            DropIndex("SportsStore.CartLines", new[] { "CartId" });
            DropIndex("SportsStore.Orders", new[] { "OrderId" });
            AddColumn("SportsStore.CartLines", "OrderId", c => c.Int(nullable: false));
            AddColumn("SportsStore.Orders", "OrderDate", c => c.DateTime(nullable: false));
            CreateIndex("SportsStore.CartLines", "OrderId");
            AddForeignKey("SportsStore.CartLines", "OrderId", "SportsStore.Orders", "OrderId", cascadeDelete: true);
            DropColumn("SportsStore.CartLines", "CartId");
            DropTable("SportsStore.Carts");
        }
        
        public override void Down()
        {
            CreateTable(
                "SportsStore.Carts",
                c => new
                    {
                        CartId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.CartId);
            
            AddColumn("SportsStore.CartLines", "CartId", c => c.Int(nullable: false));
            DropForeignKey("SportsStore.CartLines", "OrderId", "SportsStore.Orders");
            DropIndex("SportsStore.CartLines", new[] { "OrderId" });
            DropColumn("SportsStore.Orders", "OrderDate");
            DropColumn("SportsStore.CartLines", "OrderId");
            CreateIndex("SportsStore.Orders", "OrderId");
            CreateIndex("SportsStore.CartLines", "CartId");
            AddForeignKey("SportsStore.Orders", "OrderId", "SportsStore.Carts", "CartId");
            AddForeignKey("SportsStore.CartLines", "CartId", "SportsStore.Carts", "CartId", cascadeDelete: true);
        }
    }
}
