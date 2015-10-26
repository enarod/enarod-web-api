namespace Infopulse.EDemocracy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class auth_tables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "auth.App_Role",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "auth.App_UserRole",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("auth.App_Role", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("auth.App_User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);

			CreateTable(
				"auth.App_User",
				c => new
				{
					Id = c.Int(nullable: false, identity: true),
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
				.PrimaryKey(t => t.Id)
				.Index(t => t.UserName, unique: true, name: "UserNameIndex");

			CreateTable(
                "auth.App_UserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("auth.App_User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "auth.App_UserLogin",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("auth.App_User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("auth.App_UserRole", "UserId", "auth.App_User");
            DropForeignKey("auth.App_UserLogin", "UserId", "auth.App_User");
            DropForeignKey("auth.App_UserClaim", "UserId", "auth.App_User");
            DropForeignKey("auth.App_UserRole", "RoleId", "auth.App_Role");
            DropIndex("auth.App_UserLogin", new[] { "UserId" });
            DropIndex("auth.App_UserClaim", new[] { "UserId" });
            DropIndex("auth.App_User", "UserNameIndex");
            DropIndex("auth.App_UserRole", new[] { "RoleId" });
            DropIndex("auth.App_UserRole", new[] { "UserId" });
            DropIndex("auth.App_Role", "RoleNameIndex");
            DropTable("auth.App_UserLogin");
            DropTable("auth.App_UserClaim");
            DropTable("auth.App_User");
            DropTable("auth.App_UserRole");
            DropTable("auth.App_Role");
        }
    }
}
