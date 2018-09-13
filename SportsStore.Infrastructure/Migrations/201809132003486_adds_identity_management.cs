namespace SportsStore.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adds_identity_management : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "SportsStore.UserInformations",
                c => new
                    {
                        UserInformationId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        AddressLine1 = c.String(nullable: false, maxLength: 20),
                        AddressLine2 = c.String(maxLength: 20),
                        AddressLine3 = c.String(maxLength: 20),
                        AddressCity = c.String(nullable: false, maxLength: 20),
                        AddressState = c.String(nullable: false, maxLength: 20),
                        AddressZip = c.String(nullable: false, maxLength: 10),
                        AddressCountry = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.UserInformationId)
                .ForeignKey("SportsStore.AspNetUsers", t => t.UserInformationId, cascadeDelete: true)
                .Index(t => t.UserInformationId);
            
            CreateTable(
                "SportsStore.AspNetRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "SportsStore.AspNetUserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("SportsStore.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("SportsStore.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "SportsStore.AspNetUsers",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserInformationId = c.Int(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.UserId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "SportsStore.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("SportsStore.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "SportsStore.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("SportsStore.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            AddColumn("SportsStore.Orders", "UserInformationId", c => c.Int());
            CreateIndex("SportsStore.Orders", "UserInformationId");
            AddForeignKey("SportsStore.Orders", "UserInformationId", "SportsStore.UserInformations", "UserInformationId");
        }
        
        public override void Down()
        {
            DropForeignKey("SportsStore.UserInformations", "UserInformationId", "SportsStore.AspNetUsers");
            DropForeignKey("SportsStore.AspNetUserRoles", "UserId", "SportsStore.AspNetUsers");
            DropForeignKey("SportsStore.AspNetUserLogins", "UserId", "SportsStore.AspNetUsers");
            DropForeignKey("SportsStore.AspNetUserClaims", "UserId", "SportsStore.AspNetUsers");
            DropForeignKey("SportsStore.AspNetUserRoles", "RoleId", "SportsStore.AspNetRoles");
            DropForeignKey("SportsStore.Orders", "UserInformationId", "SportsStore.UserInformations");
            DropIndex("SportsStore.AspNetUserLogins", new[] { "UserId" });
            DropIndex("SportsStore.AspNetUserClaims", new[] { "UserId" });
            DropIndex("SportsStore.AspNetUsers", "UserNameIndex");
            DropIndex("SportsStore.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("SportsStore.AspNetUserRoles", new[] { "UserId" });
            DropIndex("SportsStore.AspNetRoles", "RoleNameIndex");
            DropIndex("SportsStore.UserInformations", new[] { "UserInformationId" });
            DropIndex("SportsStore.Orders", new[] { "UserInformationId" });
            DropColumn("SportsStore.Orders", "UserInformationId");
            DropTable("SportsStore.AspNetUserLogins");
            DropTable("SportsStore.AspNetUserClaims");
            DropTable("SportsStore.AspNetUsers");
            DropTable("SportsStore.AspNetUserRoles");
            DropTable("SportsStore.AspNetRoles");
            DropTable("SportsStore.UserInformations");
        }
    }
}
