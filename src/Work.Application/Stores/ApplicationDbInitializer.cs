using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Lucas.Solutions.Stores
{
    public class ApplicationDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        public static void Set()
        {
            Database.SetInitializer(new ApplicationDbInitializer());
        }

        private ApplicationDbInitializer()
        {
        }
    }
}