namespace Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration2445551 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Students", "Age", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Students", "Age", c => c.Double(nullable: false));
        }
    }
}
