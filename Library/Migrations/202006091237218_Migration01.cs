namespace Jstore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration01 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        BrandID = c.Int(nullable: false, identity: true),
                        BrandName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.BrandID);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        ItemID = c.Int(nullable: false, identity: true),
                        ItemName = c.String(nullable: false),
                        ItemType = c.String(nullable: false),
                        Stock = c.Int(nullable: false),
                        BrandID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ItemID)
                .ForeignKey("dbo.Brands", t => t.BrandID, cascadeDelete: true)
                .Index(t => t.BrandID);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        SalesID = c.Int(nullable: false, identity: true),
                        ClientID = c.Int(nullable: false),
                        ItemID = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.SalesID)
                .ForeignKey("dbo.Clients", t => t.ClientID, cascadeDelete: true)
                .ForeignKey("dbo.Items", t => t.ItemID, cascadeDelete: true)
                .Index(t => t.ClientID)
                .Index(t => t.ItemID);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ClientID = c.Int(nullable: false, identity: true),
                        ClientFirstName = c.String(nullable: false),
                        ClientLastName = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Age = c.String(),
                    })
                .PrimaryKey(t => t.ClientID);
            
            CreateTable(
                "dbo.Managers",
                c => new
                    {
                        ManagerID = c.Int(nullable: false, identity: true),
                        ManagerName = c.String(nullable: false),
                        ManagerPassword = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ManagerID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sales", "ItemID", "dbo.Items");
            DropForeignKey("dbo.Sales", "ClientID", "dbo.Clients");
            DropForeignKey("dbo.Items", "BrandID", "dbo.Brands");
            DropIndex("dbo.Sales", new[] { "ItemID" });
            DropIndex("dbo.Sales", new[] { "ClientID" });
            DropIndex("dbo.Items", new[] { "BrandID" });
            DropTable("dbo.Managers");
            DropTable("dbo.Clients");
            DropTable("dbo.Sales");
            DropTable("dbo.Items");
            DropTable("dbo.Brands");
        }
    }
}
