namespace Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        AuthorID = c.Int(nullable: false, identity: true),
                        AuthorName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.AuthorID);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        BookID = c.Int(nullable: false, identity: true),
                        BookName = c.String(nullable: false),
                        BookGenre = c.String(nullable: false),
                        Stock = c.Int(nullable: false),
                        AuthorID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookID)
                .ForeignKey("dbo.Authors", t => t.AuthorID, cascadeDelete: true)
                .Index(t => t.AuthorID);
            
            CreateTable(
                "dbo.Loans",
                c => new
                    {
                        LoansID = c.Int(nullable: false, identity: true),
                        StudentID = c.Int(nullable: false),
                        BookID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LoansID)
                .ForeignKey("dbo.Books", t => t.BookID, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentID, cascadeDelete: true)
                .Index(t => t.StudentID)
                .Index(t => t.BookID);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StudentID = c.Int(nullable: false, identity: true),
                        StudentName = c.String(nullable: false),
                        Password = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StudentID);
            
            CreateTable(
                "dbo.Managers",
                c => new
                    {
                        ManagerID = c.Int(nullable: false, identity: true),
                        ManagerName = c.String(nullable: false),
                        ManagerPassword = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ManagerID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Loans", "StudentID", "dbo.Students");
            DropForeignKey("dbo.Loans", "BookID", "dbo.Books");
            DropForeignKey("dbo.Books", "AuthorID", "dbo.Authors");
            DropIndex("dbo.Loans", new[] { "BookID" });
            DropIndex("dbo.Loans", new[] { "StudentID" });
            DropIndex("dbo.Books", new[] { "AuthorID" });
            DropTable("dbo.Managers");
            DropTable("dbo.Students");
            DropTable("dbo.Loans");
            DropTable("dbo.Books");
            DropTable("dbo.Authors");
        }
    }
}
