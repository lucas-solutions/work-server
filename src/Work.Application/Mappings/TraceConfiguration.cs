using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Lucas.Solutions.Mappings
{
    using Lucas.Solutions.Automation;

    public class TraceConfiguration : EntityTypeConfiguration<Trace>
    {
        public TraceConfiguration()
        {
            ToTable("WorkTrace");

            HasKey(trace => trace.Id);

            Property(trace => trace.Start);

            Property(trace => trace.Type)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(0x10)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("WorkTraceType")
                {
                    IsUnique = false
                })); ;
        }
    }
}