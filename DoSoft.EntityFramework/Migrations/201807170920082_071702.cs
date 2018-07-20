namespace DoSoft.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _071702 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EddoModules", "ParentId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EddoModules", "ParentId", c => c.Int(nullable: false));
        }
    }
}
