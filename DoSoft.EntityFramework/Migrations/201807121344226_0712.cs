namespace DoSoft.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0712 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EddoPermissions",
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
                .ForeignKey("dbo.EddoRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.EddoUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.EddoRoles",
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
                "dbo.EddoUserClaims",
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EddoUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.EddoUserLogin",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        UserId = c.Long(nullable: false),
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EddoUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.EddoUserLoginAttempts",
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
                "dbo.EddoUserRoles",
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
                .ForeignKey("dbo.EddoUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.EddoUsers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CreatedTime = c.DateTime(nullable: false),
                        AuthenticationSource = c.String(maxLength: 64),
                        UserName = c.String(nullable: false, maxLength: 32),
                        TenantId = c.Int(),
                        EmailAddress = c.String(nullable: false, maxLength: 256),
                        Name = c.String(nullable: false, maxLength: 32),
                        Password = c.String(nullable: false, maxLength: 128),
                        EmailConfirmationCode = c.String(maxLength: 328),
                        PasswordResetCode = c.String(maxLength: 328),
                        LockoutEndDateUtc = c.DateTime(),
                        AccessFailedCount = c.Int(nullable: false),
                        IsLockoutEnabled = c.Boolean(nullable: false),
                        PhoneNumber = c.String(),
                        IsPhoneNumberConfirmed = c.Boolean(nullable: false),
                        SecurityStamp = c.String(),
                        IsEmailConfirmed = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        LastLoginTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EddoUserRoles", "UserId", "dbo.EddoUsers");
            DropForeignKey("dbo.EddoPermissions", "UserId", "dbo.EddoUsers");
            DropForeignKey("dbo.EddoUserLogin", "UserId", "dbo.EddoUsers");
            DropForeignKey("dbo.EddoUserClaims", "UserId", "dbo.EddoUsers");
            DropForeignKey("dbo.EddoPermissions", "RoleId", "dbo.EddoRoles");
            DropIndex("dbo.EddoUserRoles", new[] { "UserId" });
            DropIndex("dbo.EddoUserLogin", new[] { "UserId" });
            DropIndex("dbo.EddoUserClaims", new[] { "UserId" });
            DropIndex("dbo.EddoPermissions", new[] { "UserId" });
            DropIndex("dbo.EddoPermissions", new[] { "RoleId" });
            DropTable("dbo.EddoUsers");
            DropTable("dbo.EddoUserRoles");
            DropTable("dbo.EddoUserLoginAttempts");
            DropTable("dbo.EddoUserLogin");
            DropTable("dbo.EddoUserClaims");
            DropTable("dbo.EddoRoles");
            DropTable("dbo.EddoPermissions");
        }
    }
}
