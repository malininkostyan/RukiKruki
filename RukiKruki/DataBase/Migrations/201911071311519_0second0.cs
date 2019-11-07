namespace DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0second0 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.RequestDetails", "DetailName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RequestDetails", "DetailName", c => c.String(nullable: false));
        }
    }
}
