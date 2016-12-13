using System.Data.Entity.Migrations;

namespace TrustSystems3.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<TSModelContainer>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TSModelContainer context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //




            







//            context.Companies.AddOrUpdate(
//                c => c.Name,
//                new Company { Name = "Sumdex" },
//                new Company { Name = "Proctor & Gamble" }
//                );
        }
    }
}
