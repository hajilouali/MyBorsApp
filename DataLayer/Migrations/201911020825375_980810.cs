namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _980810 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UsersOrderLogs",
                c => new
                    {
                        LogId = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        ContractID = c.String(maxLength: 150),
                        OrderSide = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LogId)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UsersOrderLogs", "UserID", "dbo.Users");
            DropIndex("dbo.UsersOrderLogs", new[] { "UserID" });
            DropTable("dbo.UsersOrderLogs");
        }
    }
}
