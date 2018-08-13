namespace SportsStore.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddsProductThumbnail : DbMigration
    {
        public override void Up()
        {
            AddColumn("SportsStore.Products", "ImageData", c => c.Binary());
            AddColumn("SportsStore.Products", "ImageMimeType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("SportsStore.Products", "ImageMimeType");
            DropColumn("SportsStore.Products", "ImageData");
        }
    }
}
