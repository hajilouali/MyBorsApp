namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _9808122 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UsersOrderLogs", "DateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UsersOrderLogs", "DateTime");
        }
    }
}
