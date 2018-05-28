namespace web.ef.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class order : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderDetail",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderInfoId = c.Int(nullable: false),
                        ProductName = c.String(nullable: false),
                        ProductId = c.Int(nullable: false),
                        ProductCount = c.Int(nullable: false),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OrderInfo", t => t.OrderInfoId, cascadeDelete: true)
                .Index(t => t.OrderInfoId);
            
            CreateTable(
                "dbo.OrderInfo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        No = c.String(nullable: false),
                        Status = c.String(),
                        ProductCount = c.Int(nullable: false),
                        Remark = c.String(),
                        OrderDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetail", "OrderInfoId", "dbo.OrderInfo");
            DropIndex("dbo.OrderDetail", new[] { "OrderInfoId" });
            DropTable("dbo.OrderInfo");
            DropTable("dbo.OrderDetail");
        }
    }
}
