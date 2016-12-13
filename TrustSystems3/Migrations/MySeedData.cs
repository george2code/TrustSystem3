using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustSystems3.Migrations
{
    public class MySeedData : DropCreateDatabaseIfModelChanges<TSModelContainer>
    {
        public MySeedData()
        {
            Seed(new TSModelContainer());
        }

        private void Seed(TSModelContainer context)
        {
            // Create objects here and add them to your context DBSets...
//            context.Companies.Add(new Company {Name = "Jane Austen"});
//            context.Companies.Add(new Company {Name = "Charles Dickens"});
//            context.Companies.Add(new Company {Name = "Miguel de Cervantes"});
//
//            context.SaveChanges();
        }
    }
}
