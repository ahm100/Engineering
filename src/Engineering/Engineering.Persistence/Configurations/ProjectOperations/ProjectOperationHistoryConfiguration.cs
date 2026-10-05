using Engineering.Domain.Entities.ProjectOperations;
using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Persistence.Configurations.ProjectOperations;

public class ProjectOperationHistoryConfiguration : IEntityTypeConfiguration<ProjectOperationHistory>
{
    private const string TableName = "ProjectOperationHistories";

    public void Configure(EntityTypeBuilder<ProjectOperationHistory> builder)
    {
        builder.MetaConfiguration<ProjectOperationHistory, long>(TableName);

        builder.Property(oo => oo.Workload)
            .HasComment(ProjectOperationCmts.Workload)
            .HasColumnType("decimal(18,5)")
            .IsRequired();

        builder.Property(oo => oo.UnitPrice)
            .HasComment(ProjectOperationCmts.UnitPrice)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.BasePrice)
            .HasComment(ProjectOperationCmts.BasePrice)
            .IsRequired()
            .HasDefaultValue(0m)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.ChangedPrice)
            .HasComment(ProjectOperationCmts.ChangedPrice)
            .IsRequired()
            .HasDefaultValue(0m)
            .HasColumnType("decimal(18,2)");

        builder.Property(a => a.TolerancePercentage)
            .HasComment(ProjectOperationCmts.TolerancePercentage)
            .HasColumnType("decimal(18,5)")
            .IsRequired();

        builder.Property(oo => oo.GoodsInProgress)
            .HasComment(ProjectOperationCmts.GoodsInProgress);

        builder.Property(oo => oo.Priority)
            .HasComment(ProjectOperationCmts.Priority);

        builder.Property(oo => oo.ProjectOperationStatus)
            .HasComment(ProjectOperationCmts.ProjectOperationStatus)
            .HasDefaultValue(ProjectOperationStatus.NotStarted)
            .IsRequired();

        builder.Property(oo => oo.Description)
            .HasComment(GlobalCmts.Description)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.PlannedStartDate);

        builder.Property(oo => oo.PlannedFinishDate);

        builder.Property(oo => oo.PlannedDuration);

        builder.Property(oo => oo.ActualStartDate);

        builder.Property(oo => oo.ActualFinishDate);

        builder.Property(oo => oo.BaselineStartDate);

        builder.Property(oo => oo.BaselineFinishDate);

        builder.Property(oo => oo.BaselineDuration);

        builder.HasOne(oo => oo.ProjectOperation)
            .WithMany(oo => oo.ProjectOperationHistory)
            .HasForeignKey("ProjectOperationId").HasPrincipalKey(nameof(ProjectOperation.Id))
            .OnDelete(DeleteBehavior.NoAction);
    }
}