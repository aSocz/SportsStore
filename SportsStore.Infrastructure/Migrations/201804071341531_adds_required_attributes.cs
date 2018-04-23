namespace SportsStore.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adds_required_attributes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("SportsStore.Orders", "AddressLine1", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("SportsStore.Orders", "AddressCity", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("SportsStore.Orders", "AddressState", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("SportsStore.Orders", "AddressZip", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("SportsStore.Orders", "AddressCountry", c => c.String(nullable: false, maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("SportsStore.Orders", "AddressCountry", c => c.String(maxLength: 20));
            AlterColumn("SportsStore.Orders", "AddressZip", c => c.String(maxLength: 10));
            AlterColumn("SportsStore.Orders", "AddressState", c => c.String(maxLength: 20));
            AlterColumn("SportsStore.Orders", "AddressCity", c => c.String(maxLength: 20));
            AlterColumn("SportsStore.Orders", "AddressLine1", c => c.String(maxLength: 20));
        }
    }
}
