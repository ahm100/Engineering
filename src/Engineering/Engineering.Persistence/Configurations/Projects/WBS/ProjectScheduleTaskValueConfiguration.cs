using Engineering.Domain.Entities.Projects.WBS;

namespace Engineering.Persistence.Configurations.Projects.WBS;

public class ProjectScheduleTaskValueConfiguration
    : IEntityTypeConfiguration<ProjectScheduleTaskValue>
{
    private const string TableName = "ProjectScheduleTaskValues";

    public void Configure(EntityTypeBuilder<ProjectScheduleTaskValue> builder)
    {
        builder.MetaActiveConfiguration<ProjectScheduleTaskValue, long>(TableName);

        builder.Property(x => x.Value)
            .HasComment(GlobalCmts.Value)
            .HasMaxLength(1500);

        builder.Property(x => x.NumberValue)
            .HasComment(WbsCmts.NumberValue)
            .HasPrecision(16, 2);

        builder.Property(x => x.DateTimeValue)
            .HasComment(WbsCmts.DateTimeValue);

        builder.HasOne(x => x.ProjectScheduleTask)
            .WithMany(x => x.ProjectScheduleTaskValues)
            .HasForeignKey(x => x.ProjectScheduleTaskId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        builder.HasOne(x => x.ProjectScheduleColumn)
            .WithMany(x => x.ProjectScheduleTaskValues)
            .HasForeignKey(x => x.ProjectScheduleColumnId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();
    }
}