namespace DoSoft.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0718 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EddoModules", "Resource", c => c.String());
            AddColumn("dbo.EddoModules", "PermissionNames", c => c.String());
            AddColumn("dbo.EddoModules", "controller", c => c.String());
            AddColumn("dbo.EddoModules", "action", c => c.String());
            AddColumn("dbo.EddoModules", "IconClass", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EddoModules", "IconClass");
            DropColumn("dbo.EddoModules", "action");
            DropColumn("dbo.EddoModules", "controller");
            DropColumn("dbo.EddoModules", "PermissionNames");
            DropColumn("dbo.EddoModules", "Resource");
        }
    }
}
