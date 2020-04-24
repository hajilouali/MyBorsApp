namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _980728 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserContracts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        ContractID1 = c.Int(nullable: false),
                        ContractID2 = c.Int(nullable: false),
                        Max = c.Double(nullable: false),
                        Min = c.Double(nullable: false),
                        Ave = c.Double(nullable: false),
                        AveVazni = c.Double(nullable: false),
                        Damagelimit = c.Double(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserContracts", "UserID", "dbo.Users");
            DropIndex("dbo.UserContracts", new[] { "UserID" });
            DropTable("dbo.UserContracts");
        }
    }
}
