using Engineering.Domain.Entities.Tasks;
using Engineering.Domain.Entities.Tasks.Enums;

namespace Engineering.Persistence.Configurations.Tasks;

public class UserTaskConfiguration : IEntityTypeConfiguration<UserTask>
{
    private const string TableName = "UserTasks";

    public void Configure(EntityTypeBuilder<UserTask> builder)
    {
        builder.MetaActiveConfiguration<UserTask, long>(TableName);

        builder.Property(x => x.Title)
            .HasMaxLength(250)
            .IsUnicode(true)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(1000)
            .IsUnicode(true)
            .IsRequired(false);
      
        builder.Property(x => x.OwnerUserId)
            .IsRequired(false);

        builder.HasOne(x => x.TaskGroup)
            .WithMany(x => x.UserTasks)
            .HasForeignKey(x => x.TaskGroupId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}