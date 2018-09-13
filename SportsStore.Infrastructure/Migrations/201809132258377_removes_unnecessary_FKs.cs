namespace SportsStore.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removes_unnecessary_FKs : DbMigration
    {
        public override void Up()
        {
            DropColumn("SportsStore.UserInformations", "UserId");
            DropColumn("SportsStore.AspNetUsers", "UserInformationId");
        }
        
        public override void Down()
        {
            AddColumn("SportsStore.AspNetUsers", "UserInformationId", c => c.Int(nullable: false));
            AddColumn("SportsStore.UserInformations", "UserId", c => c.Int(nullable: false));
        }
    }
}
