using Engineering.Domain.Entities.CostCenters;
using Engineering.Domain.Entities.ProjectOperations;
using Engineering.Domain.Entities.ProjectOperations.Enums;
using Engineering.Domain.Entities.Projects;

namespace Engineering.Persistence.Configurations.ProjectOperations;

public class ProjectOperationTemporaryDailyConfiguration : IEntityTypeConfiguration<ProjectOperationTemporaryDaily>
{
    private const string _tableName = "ProjectOperationTemporaryDailies";
    public void Configure(EntityTypeBuilder<ProjectOperationTemporaryDaily> builder)
    {
        builder.ToTable(_tableName);
        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.StartDate)
            .IsRequired();

        builder.Property(oo => oo.EndDate)
            .IsRequired();

        builder.Property(oo => oo.Description)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.Status)
            .HasDefaultValue(TemporaryDailyStatus.NotStarted);

        builder.Property(oo => oo.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(oo => oo.Created)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(oo => oo.CreatorId)
            .IsRequired();

        builder.HasOne(oo => oo.ProjectOperation)
            .WithMany(oo => oo.ProjectOperationTemporaryDailies)
            .HasForeignKey("ProjectOperationId").HasPrincipalKey(nameof(ProjectOperation.Id))
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(oo => oo.Project)
            .WithMany(oo => oo.ProjectOperationTemporaryDailies)
            .HasForeignKey("ProjectId").HasPrincipalKey(nameof(Project.Id))
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(oo => oo.CostCenter)
            .WithMany(oo => oo.ProjectOperationTemporaryDailies)
            .HasForeignKey("CostCenterId").HasPrincipalKey(nameof(CostCenter.Id))
            .OnDelete(DeleteBehavior.NoAction);
    }
}