namespace DoSoft.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _071802 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EddoModules", "area", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EddoModules", "area");
        }
    }
}
