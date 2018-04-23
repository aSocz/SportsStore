namespace SportsStore.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddsEmailToOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("SportsStore.Orders", "ClientEmail", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("SportsStore.Orders", "ClientEmail");
        }
    }
}
