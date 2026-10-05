using Engineering.Domain.Entities.Tasks;

namespace Engineering.Persistence.Configurations.Tasks;

public class TaskGroupConfiguration : IEntityTypeConfiguration<TaskGroup>
{
    private const string TableName = "TaskGroups";

    public void Configure(EntityTypeBuilder<TaskGroup> builder)
    {
        builder.MetaActiveConfiguration<TaskGroup, long>(TableName);

        builder.Property(x => x.Title)
            .HasMaxLength(250)
            .IsUnicode(true)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(1000)
            .IsUnicode(true)
            .IsRequired(false);
    }
}