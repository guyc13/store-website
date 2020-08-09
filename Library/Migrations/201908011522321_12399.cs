namespace Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _12399 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Loans", "peopel", c => c.String());
            DropColumn("dbo.Loans", "name99");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Loans", "name99", c => c.String());
            DropColumn("dbo.Loans", "peopel");
        }
    }
}
