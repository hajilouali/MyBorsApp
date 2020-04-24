namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _980828888 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContractCellslogs", "DateTimeCreatLog", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContractCellslogs", "DateTimeCreatLog");
        }
    }
}
