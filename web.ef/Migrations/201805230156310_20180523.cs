namespace web.ef.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20180523 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UserLogins", newName: "UserLogin");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.UserLogin", newName: "UserLogins");
        }
    }
}
