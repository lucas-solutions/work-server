using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Lucas.Solutions.Mappings
{
    using Lucas.Solutions.IO;

    public class IncomingTraceConfiguration : EntityTypeConfiguration<IncomingTrace>
    {
        public IncomingTraceConfiguration()
        {
            ToTable("WorkIncomingTrace");

            HasRequired(incoming => incoming.Sender)
                .WithMany(outgoing => outgoing.Recipients)
                .HasForeignKey(incoming => incoming.SenderId)
                .WillCascadeOnDelete(false);
        }
    }
}