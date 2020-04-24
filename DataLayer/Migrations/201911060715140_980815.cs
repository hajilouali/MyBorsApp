namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _980815 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserContracts", "ContractType", c => c.Int(nullable: false));
            AddColumn("dbo.UsersOrderLogs", "Contract1Valum", c => c.Int(nullable: false));
            AddColumn("dbo.UsersOrderLogs", "Contract1Price", c => c.Int(nullable: false));
            AddColumn("dbo.UsersOrderLogs", "Contract2Valum", c => c.Int(nullable: false));
            AddColumn("dbo.UsersOrderLogs", "contract2price", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UsersOrderLogs", "contract2price");
            DropColumn("dbo.UsersOrderLogs", "Contract2Valum");
            DropColumn("dbo.UsersOrderLogs", "Contract1Price");
            DropColumn("dbo.UsersOrderLogs", "Contract1Valum");
            DropColumn("dbo.UserContracts", "ContractType");
        }
    }
}
