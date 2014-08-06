using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Lucas.Solutions.Mappings
{
    using Lucas.Solutions.IO;

    public class OutgoingTraceConfiguration : EntityTypeConfiguration<OutgoingTrace>
    {
        public OutgoingTraceConfiguration()
        {
            ToTable("WorkOutgoingTrace");
        }
    }
}