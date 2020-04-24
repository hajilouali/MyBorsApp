namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _980726 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "UserSetToken", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "UserSetToken");
        }
    }
}
