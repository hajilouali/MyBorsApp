namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _9808011 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserContracts", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserContracts", "IsActive");
        }
    }
}
