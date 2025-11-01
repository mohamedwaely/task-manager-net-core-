using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using task_manager.Data;

namespace task_manager.ModelConfig
{
    public class TaskEntityConfig : IEntityTypeConfiguration<Tasks>
    {
        void IEntityTypeConfiguration<Tasks>.Configure(EntityTypeBuilder<Tasks> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Dscription).IsRequired();
            builder.Property(t => t.status).IsRequired();
            builder.Property(t => t.CreatedAt).IsRequired();
            builder.HasOne(t => t.user)
                .WithMany(u => u.tasks)
                .HasForeignKey(t => t.UserId)
                .IsRequired();
        }
        
    }
}
