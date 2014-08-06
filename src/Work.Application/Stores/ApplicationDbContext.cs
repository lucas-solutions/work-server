using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Lucas.Solutions.Stores
{
    using Lucas.Solutions.Mappings;
    using Lucas.Solutions.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations
                .Add(new HostConfiguration())
                .Add(new TaskConfiguration())
                .Add(new TransferConfiguration())
                .Add(new PartyConfiguration())
                .Add(new TraceConfiguration())
                .Add(new TransferTraceConfiguration())
                .Add(new OutgoingTraceConfiguration())
                .Add(new IncomingTraceConfiguration());
        }
    }
}