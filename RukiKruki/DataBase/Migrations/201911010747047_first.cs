namespace DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        VIN = c.String(nullable: false),
                        Brand = c.String(nullable: false),
                        Model = c.String(nullable: false),
                        Mileage = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientFIO = c.String(nullable: false),
                        Login = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Mail = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        TotalSum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrderStatus = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateImplement = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.OrderTOes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TOId = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
                        Amount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.TOes", t => t.TOId, cascadeDelete: true)
                .Index(t => t.TOId)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.TOes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TOName = c.String(nullable: false),
                        Price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TO_Detail",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TOId = c.Int(nullable: false),
                        DetailId = c.Int(nullable: false),
                        Amount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Details", t => t.DetailId, cascadeDelete: true)
                .ForeignKey("dbo.TOes", t => t.TOId, cascadeDelete: true)
                .Index(t => t.TOId)
                .Index(t => t.DetailId);
            
            CreateTable(
                "dbo.Details",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DetailName = c.String(nullable: false),
                        Amount = c.Int(nullable: false),
                        Reserve = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RequestDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DetailId = c.Int(nullable: false),
                        RequestId = c.Int(nullable: false),
                        Amount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Details", t => t.DetailId, cascadeDelete: true)
                .ForeignKey("dbo.Requests", t => t.RequestId, cascadeDelete: true)
                .Index(t => t.DetailId)
                .Index(t => t.RequestId);
            
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateCreate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TO_Detail", "TOId", "dbo.TOes");
            DropForeignKey("dbo.TO_Detail", "DetailId", "dbo.Details");
            DropForeignKey("dbo.RequestDetails", "RequestId", "dbo.Requests");
            DropForeignKey("dbo.RequestDetails", "DetailId", "dbo.Details");
            DropForeignKey("dbo.OrderTOes", "TOId", "dbo.TOes");
            DropForeignKey("dbo.OrderTOes", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.Cars", "ClientId", "dbo.Clients");
            DropIndex("dbo.RequestDetails", new[] { "RequestId" });
            DropIndex("dbo.RequestDetails", new[] { "DetailId" });
            DropIndex("dbo.TO_Detail", new[] { "DetailId" });
            DropIndex("dbo.TO_Detail", new[] { "TOId" });
            DropIndex("dbo.OrderTOes", new[] { "OrderId" });
            DropIndex("dbo.OrderTOes", new[] { "TOId" });
            DropIndex("dbo.Orders", new[] { "ClientId" });
            DropIndex("dbo.Cars", new[] { "ClientId" });
            DropTable("dbo.Requests");
            DropTable("dbo.RequestDetails");
            DropTable("dbo.Details");
            DropTable("dbo.TO_Detail");
            DropTable("dbo.TOes");
            DropTable("dbo.OrderTOes");
            DropTable("dbo.Orders");
            DropTable("dbo.Clients");
            DropTable("dbo.Cars");
        }
    }
}
