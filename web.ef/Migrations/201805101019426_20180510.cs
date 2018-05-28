namespace web.ef.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20180510 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.User", newName: "Users");
            DropPrimaryKey("dbo.Users");
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        Name = c.String(nullable: false, maxLength: 128),
                        IsGranted = c.Boolean(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                        CreateUserId = c.String(),
                        RoleId = c.Int(),
                        UserId = c.Long(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        TenantId = c.Int(),
                        Name = c.String(nullable: false, maxLength: 32),
                        DisplayName = c.String(nullable: false, maxLength: 64),
                        IsStatic = c.Boolean(nullable: false),
                        IsDefault = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserClaims",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        UserId = c.Long(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        CreatedTime = c.DateTime(nullable: false),
                        CreateUserId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserLogins",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        UserId = c.Long(nullable: false),
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserLoginAttempts",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        TenancyName = c.String(maxLength: 128),
                        UserId = c.Long(),
                        UserNameOrEmailAddress = c.String(maxLength: 255),
                        ClientIpAddress = c.String(maxLength: 64),
                        ClientName = c.String(maxLength: 128),
                        BrowserInfo = c.String(maxLength: 256),
                        Result = c.Byte(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        UserId = c.Long(nullable: false),
                        RoleId = c.Int(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                        CreateUserId = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            AddColumn("dbo.Users", "CreatedTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "AuthenticationSource", c => c.String(maxLength: 64));
            AddColumn("dbo.Users", "TenantId", c => c.Int());
            AddColumn("dbo.Users", "EmailAddress", c => c.String(nullable: false, maxLength: 256));
            AddColumn("dbo.Users", "Name", c => c.String(nullable: false, maxLength: 32));
            AddColumn("dbo.Users", "Password", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Users", "EmailConfirmationCode", c => c.String(maxLength: 328));
            AddColumn("dbo.Users", "PasswordResetCode", c => c.String(maxLength: 328));
            AddColumn("dbo.Users", "LockoutEndDateUtc", c => c.DateTime());
            AddColumn("dbo.Users", "AccessFailedCount", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "IsLockoutEnabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "PhoneNumber", c => c.String());
            AddColumn("dbo.Users", "IsPhoneNumberConfirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "SecurityStamp", c => c.String());
            AddColumn("dbo.Users", "IsEmailConfirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "LastLoginTime", c => c.DateTime());
            AlterColumn("dbo.Users", "Id", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.Users", "UserName", c => c.String(nullable: false, maxLength: 32));
            AddPrimaryKey("dbo.Users", "Id");
            DropColumn("dbo.Users", "Address");
            DropColumn("dbo.Users", "Tel");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Tel", c => c.String());
            AddColumn("dbo.Users", "Address", c => c.String());
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.Permissions", "RoleId", "dbo.Roles");
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.Permissions", new[] { "RoleId" });
            DropPrimaryKey("dbo.Users");
            AlterColumn("dbo.Users", "UserName", c => c.String());
            AlterColumn("dbo.Users", "Id", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Users", "LastLoginTime");
            DropColumn("dbo.Users", "IsActive");
            DropColumn("dbo.Users", "IsEmailConfirmed");
            DropColumn("dbo.Users", "SecurityStamp");
            DropColumn("dbo.Users", "IsPhoneNumberConfirmed");
            DropColumn("dbo.Users", "PhoneNumber");
            DropColumn("dbo.Users", "IsLockoutEnabled");
            DropColumn("dbo.Users", "AccessFailedCount");
            DropColumn("dbo.Users", "LockoutEndDateUtc");
            DropColumn("dbo.Users", "PasswordResetCode");
            DropColumn("dbo.Users", "EmailConfirmationCode");
            DropColumn("dbo.Users", "Password");
            DropColumn("dbo.Users", "Name");
            DropColumn("dbo.Users", "EmailAddress");
            DropColumn("dbo.Users", "TenantId");
            DropColumn("dbo.Users", "AuthenticationSource");
            DropColumn("dbo.Users", "CreatedTime");
            DropTable("dbo.UserRoles");
            DropTable("dbo.UserLoginAttempts");
            DropTable("dbo.UserLogins");
            DropTable("dbo.UserClaims");
            DropTable("dbo.Roles");
            DropTable("dbo.Permissions");
            AddPrimaryKey("dbo.Users", "Id");
            RenameTable(name: "dbo.Users", newName: "User");
        }
    }
}
