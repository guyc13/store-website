namespace Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration244555 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "Age", c => c.Double(nullable: false));
            AlterColumn("dbo.Students", "Password", c => c.String(nullable: false));
            AlterColumn("dbo.Managers", "ManagerPassword", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Managers", "ManagerPassword", c => c.Int(nullable: false));
            AlterColumn("dbo.Students", "Password", c => c.Int(nullable: false));
            DropColumn("dbo.Students", "Age");
        }
    }
}
