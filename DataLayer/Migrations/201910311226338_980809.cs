namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _980809 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserContracts", "ContractCountActive", c => c.Int(nullable: false));
            AlterColumn("dbo.UserContracts", "Max", c => c.Int(nullable: false));
            AlterColumn("dbo.UserContracts", "Min", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserContracts", "Min", c => c.Double(nullable: false));
            AlterColumn("dbo.UserContracts", "Max", c => c.Double(nullable: false));
            DropColumn("dbo.UserContracts", "ContractCountActive");
        }
    }
}
