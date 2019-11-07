namespace DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RequestDetails", "DetailName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RequestDetails", "DetailName");
        }
    }
}
