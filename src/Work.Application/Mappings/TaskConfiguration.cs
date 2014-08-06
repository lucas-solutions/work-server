using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Lucas.Solutions.Mappings
{
    using Lucas.Solutions.Automation;

    public class TaskConfiguration : EntityTypeConfiguration<Task>
    {
        public TaskConfiguration()
        {
            ToTable("WorkTask");

            HasKey(task => task.Id);

            Property(task => task.Name)
                .IsRequired()
                .IsUnicode(true)
                .HasMaxLength(0x20)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("WorkTaskName")
                    {
                        IsUnique = true
                    }));

            Property(task => task.Start);

            Property(task => task.State);

            Property(task => task.Summary)
                .IsOptional()
                .IsUnicode(true)
                .HasMaxLength(0x80);

            Property(task => task.Type).IsRequired()
                .IsUnicode(false)
                .HasMaxLength(0x10)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("WorkTaskType")
                {
                    IsUnique = false
                })); ;
        }
    }
}