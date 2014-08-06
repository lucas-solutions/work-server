using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Lucas.Solutions.Mappings
{
    using Lucas.Solutions.IO;

    public class HostConfiguration : EntityTypeConfiguration<Host>
    {
        public HostConfiguration()
        {
            ToTable("WorkHost");

            HasKey(host => host.Id);

            Property(host => host.Address)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(0x40)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("WorkHostAddress")
                {
                    IsUnique = true
                }));

            Property(host => host.Credential)
                .IsOptional()
                .IsUnicode(false)
                .HasMaxLength(0x40);

            Property(host => host.Password)
                .IsOptional()
                .IsUnicode(false)
                .HasMaxLength(0x40);

            Property(host => host.Port);

            Property(host => host.Protocol);

            Property(host => host.Summary)
                .IsOptional()
                .IsUnicode(true)
                .HasMaxLength(0x80);
        }
    }
}