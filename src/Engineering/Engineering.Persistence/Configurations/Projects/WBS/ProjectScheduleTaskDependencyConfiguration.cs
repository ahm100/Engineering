using Engineering.Domain.Entities.Projects.WBS;

namespace Engineering.Persistence.Configurations.Projects.WBS;

public class ProjectScheduleTaskDependencyConfiguration
    : IEntityTypeConfiguration<ProjectScheduleTaskDependency>
{
    private const string TableName = "ProjectScheduleTaskDependencies";

    public void Configure(EntityTypeBuilder<ProjectScheduleTaskDependency> builder)
    {
        builder.MetaConfiguration<ProjectScheduleTaskDependency, long>(TableName);

        builder.Property(x => x.Type)
            .HasComment(WbsCmts.ProjectScheduleDependencyType)
            .IsRequired();

        builder.Property(x => x.LagMinutes)
            .HasComment(WbsCmts.LagMinutes)
            .IsRequired();

        builder.HasOne(x => x.PredecessorTask)
            .WithMany(x => x.SuccessorDependencies)
            .HasForeignKey(x => x.PredecessorTaskId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        builder.HasOne(x => x.SuccessorTask)
            .WithMany(x => x.PredecessorDependencies)
            .HasForeignKey(x => x.SuccessorTaskId)
            .OnDelete(DeleteBehavior.NoAction);

    }
}