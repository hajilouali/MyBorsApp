namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _98081000 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UsersOrderLogs", "ContractID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UsersOrderLogs", "ContractID", c => c.String(maxLength: 150));
        }
    }
}
