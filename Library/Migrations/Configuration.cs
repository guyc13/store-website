namespace Library.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<NewLibrary.Models.DBLibrary>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "NewLibrary.Models.DBLibrary";
        }

        protected override void Seed(NewLibrary.Models.DBLibrary context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
