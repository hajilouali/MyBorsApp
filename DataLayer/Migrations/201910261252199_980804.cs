namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _980804 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserContracts", "ContContract", c => c.Int(nullable: false));
            DropColumn("dbo.UserContracts", "Ave");
            DropColumn("dbo.UserContracts", "AveVazni");
            DropColumn("dbo.UserContracts", "Damagelimit");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserContracts", "Damagelimit", c => c.Double(nullable: false));
            AddColumn("dbo.UserContracts", "AveVazni", c => c.Double(nullable: false));
            AddColumn("dbo.UserContracts", "Ave", c => c.Double(nullable: false));
            DropColumn("dbo.UserContracts", "ContContract");
        }
    }
}
