using Engineering.Domain.Entities.OperationInfos;
using Engineering.Domain.Entities.ProjectOperations;
using Engineering.Domain.Entities.ProjectOperations.Enums;
using Engineering.Domain.Entities.Projects;

namespace Engineering.Persistence.Configurations.ProjectOperations;

public class ProjectOperationConfiguration : IEntityTypeConfiguration<ProjectOperation>
{
    private const string _tableName = "ProjectOperations";
    public void Configure(EntityTypeBuilder<ProjectOperation> builder)
    {
        builder.ToTable(_tableName);
        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.Workload)
            .HasComment(ProjectOperationCmts.Workload)
            .HasColumnType("decimal(18,5)")
            .IsRequired();

        builder.Property(a => a.TolerancePercentage)
            .HasComment(ProjectOperationCmts.TolerancePercentage)
            .HasColumnType("decimal(18,5)")
            .IsRequired();

        builder.Property(oo => oo.IncreaseRate)
            .HasColumnType("decimal(5,2)")
            .HasComment(EContractCmts.IncreaseRate)
            .HasDefaultValue(1)
            .IsRequired();

        builder.Property(oo => oo.Price)
            .HasComment(ProjectOperationCmts.Price)
            .HasDefaultValue(0m)
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

        builder.Property(oo => oo.UnitOfMeasurementId)
            .HasComment(ProjectOperationCmts.UnitOfMeasurementId)
            .IsRequired();

        builder.Property(oo => oo.GoodsInProgress)
        .HasComment(ProjectOperationCmts.GoodsInProgress);

        builder.Property(oo => oo.CompanyId)
        .HasComment(GlobalCmts.CompanyId);

        builder.Property(oo => oo.Priority)
        .HasComment(ProjectOperationCmts.Priority);

        builder.Property(oo => oo.ProjectOperationStatus)
            .HasComment(ProjectOperationCmts.ProjectOperationStatus)
            .HasDefaultValue(ProjectOperationStatus.NotStarted)
            .IsRequired();

        builder.Property(oo => oo.Description)
            .HasComment(GlobalCmts.Description)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.IsDeleted)
            .HasComment(GlobalCmts.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(oo => oo.Created)
            .HasComment(GlobalCmts.Created)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(oo => oo.CreatorId)
            .HasComment(GlobalCmts.CreatorId)
            .IsRequired();


        builder.Property(oo => oo.PlannedStartDate);

        builder.Property(oo => oo.PlannedFinishDate);

        builder.Property(oo => oo.PlannedDuration);

        builder.Property(oo => oo.ActualStartDate);

        builder.Property(oo => oo.ActualFinishDate);

        builder.Property(oo => oo.BaselineStartDate);

        builder.Property(oo => oo.BaselineFinishDate);

        builder.Property(oo => oo.BaselineDuration);

        //builder.HasOne(oo => oo.EmployerContract)
        //    .WithMany(oo => oo.EmployerOperations)
        //    .HasForeignKey("EmployerContractId").HasPrincipalKey(nameof(EmployerContract.Id))
        //    .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(oo => oo.OperationInfo)
            .WithMany(oo => oo.ProjectOperations)
            .HasForeignKey("OperationInfoId").HasPrincipalKey(nameof(OperationInfo.Id))
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(a => a.Project)
            .WithMany(a => a.ProjectOperations)
            .HasForeignKey("ProjectId").HasPrincipalKey(nameof(Project.Id))
            .OnDelete(DeleteBehavior.NoAction);
    }
}