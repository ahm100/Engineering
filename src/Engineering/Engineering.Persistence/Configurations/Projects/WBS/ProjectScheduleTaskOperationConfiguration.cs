using Engineering.Domain.Entities.Projects.WBS;

namespace Engineering.Persistence.Configurations.Projects.WBS;

public class ProjectScheduleTaskOperationConfiguration
    : IEntityTypeConfiguration<ProjectScheduleTaskOperation>
{
    private const string TableName = "ProjectScheduleTaskOperations";

    public void Configure(EntityTypeBuilder<ProjectScheduleTaskOperation> builder)
    {
        builder.MetaConfiguration<ProjectScheduleTaskOperation, long>(TableName);


        builder.HasOne(x => x.ProjectScheduleTask)
            .WithMany(x => x.ProjectScheduleTaskOperations)
            .HasForeignKey(x => x.ProjectScheduleTaskId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        builder.HasOne(x => x.ProjectOperation)
            .WithMany(x => x.ProjectScheduleTaskOperations)
            .HasForeignKey(x => x.ProjectOperationId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

    }
}