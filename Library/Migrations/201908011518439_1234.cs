namespace Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1234 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Loans", "name99", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Loans", "name99");
        }
    }
}
