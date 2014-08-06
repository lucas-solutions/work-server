using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Lucas.Solutions.Mappings
{
    using Lucas.Solutions.IO;

    public class PartyConfiguration : EntityTypeConfiguration<Party>
    {
        public PartyConfiguration()
        {
            ToTable("WorkParty");

            HasKey(party => party.Id);

            HasRequired(party => party.Host)
                .WithMany()
                .HasForeignKey(party => party.HostId)
                .WillCascadeOnDelete(false);

            HasRequired(party => party.Transfer)
                .WithMany(transfer => transfer.Parties)
                .HasForeignKey(party => party.TransferId)
                .WillCascadeOnDelete(true);

            Property(party => party.Direction);

            Property(party => party.Email)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(0x40)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("WorkPartyEmail")
                {
                    IsUnique = false
                }));

            Property(party => party.Credential)
                .IsOptional()
                .IsUnicode(false)
                .HasMaxLength(0x40);

            Property(party => party.Name)
                .IsRequired()
                .IsUnicode(true)
                .HasMaxLength(0x20)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("WorkPartyName")
                {
                    IsUnique = true
                }));

            Property(party => party.Password)
                .IsOptional()
                .IsUnicode(false)
                .HasMaxLength(0x40);

            Property(party => party.Path)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(0x40);

            Property(party => party.Summary)
                .IsOptional()
                .IsUnicode(true)
                .HasMaxLength(0x80);
        }
    }
}