namespace SportsStore.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adds_product_soft_delete : DbMigration
    {
        public override void Up()
        {
            AddColumn("SportsStore.Products", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("SportsStore.Products", "IsActive");
        }
    }
}
