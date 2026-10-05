using Engineering.Domain.Entities.Projects.WBS;
using MathNet.Numerics.Interpolation;

namespace Engineering.Persistence.Configurations.Projects.WBS;

public class ProjectScheduleTaskConfiguration
    : IEntityTypeConfiguration<ProjectScheduleTask>
{
    private const string TableName = "ProjectScheduleTasks";

    public void Configure(EntityTypeBuilder<ProjectScheduleTask> builder)
    {
        builder.MetaConfiguration<ProjectScheduleTask, long>(TableName);

        builder.Property(x => x.Title)
            .HasComment(GlobalCmts.Title)
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(x => x.MppUid)
            .HasComment(WbsCmts.MppUid);

        builder.Property(x => x.MppId)
            .HasComment(WbsCmts.MppId);

        builder.Property(x => x.SortOrder)
            .HasComment(WbsCmts.SortOrder);

        builder.Property(x => x.OutlineLevel)
            .HasComment(WbsCmts.OutlineLevel);

        builder.Property(x => x.OutlineNumber)
            .HasComment(WbsCmts.OutlineNumber)
            .HasMaxLength(50);

        builder.Property(x => x.PlannedStart)
            .HasComment(WbsCmts.PlannedStart);

        builder.Property(x => x.PlannedFinish)
            .HasComment(WbsCmts.PlannedFinish);

        builder.Property(x => x.PlannedDurationMinutes)
            .HasComment(WbsCmts.PlannedDurationMinutes);

        builder.Property(x => x.PercentComplete)
            .HasComment(WbsCmts.PercentComplete)
            .HasPrecision(5, 2);

        builder.Property(x => x.BaselineStart)
            .HasComment(WbsCmts.BaselineStart);

        builder.Property(x => x.BaselineFinish)
            .HasComment(WbsCmts.BaselineFinish);

        builder.Property(x => x.BaselineDurationMinutes)
            .HasComment(WbsCmts.BaselineDurationMinutes);

        builder.Property(x => x.ActualStart)
            .HasComment(WbsCmts.ActualStart);

        builder.Property(x => x.ActualFinish)
            .HasComment(WbsCmts.ActualFinish);

        builder.Property(x => x.ActualDurationMinutes)
            .HasComment(WbsCmts.ActualDurationMinutes);

        builder.Property(x => x.IsMilestone)
            .HasComment(WbsCmts.IsMilestone);

        builder.Property(x => x.IsCritical)
            .HasComment(WbsCmts.IsCritical);

        builder.Property(x => x.IsSummary)
            .HasComment(WbsCmts.IsSummary);

        builder.Property(x => x.IsManuallyScheduled)
            .HasComment(WbsCmts.IsManuallyScheduled);

        builder.HasOne(x => x.ProjectScheduleImport)
            .WithMany(x => x.ProjectScheduleTasks)
            .HasForeignKey(x => x.ProjectScheduleImportId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        builder.Property(x => x.RemainingDurationMinutes)
            .HasComment(WbsCmts.RemainingDurationMinutes);

        builder.Property(x => x.PhysicalPercentComplete)
            .HasComment(WbsCmts.PhysicalPercentComplete)
            .HasPrecision(5, 2);

        builder.HasOne(x => x.Parent)
            .WithMany(x => x.Childs)
            .HasForeignKey(x => x.ParentId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Navigation(x => x.Childs)
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.HasOne(x => x.Calendar)
            .WithMany(x => x.ProjectScheduleTasks)
            .HasForeignKey(x => x.CalendarId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.ProjectWbs)
            .WithMany(x => x.ProjectScheduleTasks)
            .HasForeignKey(x => x.ProjectWbsId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Property(x => x.IsEstimated)
            .HasComment(WbsCmts.IsEstimated);

        builder.Property(x => x.Deadline)
            .HasComment(WbsCmts.Deadline);

        builder.Property(x => x.Cost)
            .HasComment(WbsCmts.Cost)
            .HasPrecision(18, 2);

        builder.Property(x => x.Weight)
            .HasComment(GlobalCmts.Weight)
            .HasPrecision(18, 2);

        builder.Property(x => x.Note)
            .HasComment(WbsCmts.Note)
            .HasMaxLength(1500);

        builder.HasIndex(x => new
        {
            x.ProjectScheduleImportId,
            x.MppUid
        })
        .IsUnique()
        .HasFilter("[MppUid] IS NOT NULL");
    }
}