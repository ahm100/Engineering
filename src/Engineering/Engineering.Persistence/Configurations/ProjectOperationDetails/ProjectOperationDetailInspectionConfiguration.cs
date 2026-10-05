using Engineering.Domain.Entities.OperationInfos;
using Engineering.Domain.Entities.OperationLocations;
using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperations;
using Engineering.Domain.Entities.Projects;

namespace Engineering.Persistence.Configurations.ProjectOperationDetails;

public class ProjectOperationDetailInspectionConfiguration : IEntityTypeConfiguration<ProjectOperationDetailInspection>
{
    private const string _tableName = "ProjectOperationDetailInspections";
    public void Configure(EntityTypeBuilder<ProjectOperationDetailInspection> builder)
    {
        builder.ToTable(_tableName);
        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.Length)
            .HasColumnType("decimal(18,5)")
            .HasDefaultValue(1)
            .IsRequired();

        builder.Property(oo => oo.Width)
            .HasColumnType("decimal(18,5)")
            .HasDefaultValue(1)
            .IsRequired();

        builder.Property(oo => oo.Height)
            .HasColumnType("decimal(18,5)")
            .HasDefaultValue(1)
            .IsRequired();

        builder.Property(oo => oo.Weight)
            .HasColumnType("decimal(18,5)")
            .HasDefaultValue(1)
            .IsRequired();

        builder.Property(oo => oo.Number)
            .HasColumnType("decimal(18,5)")
            .HasDefaultValue(1)
            .IsRequired();

        builder.Property(oo => oo.InspectionDate);

        builder.Property(oo => oo.Description)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.Created)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(oo => oo.CreatorId)
            .IsRequired();

        builder.HasOne(oo => oo.Project)
            .WithMany(oo => oo.ProjectOperationDetailInspections)
            .HasForeignKey("ProjectId")
            .HasPrincipalKey(nameof(Project.Id))
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(oo => oo.OperationInfo)
            .WithMany(oo => oo.ProjectOperationDetailInspections)
            .HasForeignKey("OperationInfoId")
            .HasPrincipalKey(nameof(OperationInfo.Id))
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(oo => oo.OperationLocation)
            .WithMany(oo => oo.ProjectOperationDetailInspections)
            .HasForeignKey("OperationLocationId")
            .HasPrincipalKey(nameof(OperationLocation.Id))
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(oo => oo.ProjectOperation)
            .WithMany(oo => oo.ProjectOperationDetailInspections)
            .HasForeignKey("ProjectOperationId")
            .HasPrincipalKey(nameof(ProjectOperation.Id))
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(oo => oo.ProjectOperationDetail)
            .WithMany(oo => oo.ProjectOperationDetailInspections)
            .HasForeignKey("ProjectOperationDetailId")
            .HasPrincipalKey(nameof(ProjectOperationDetail.Id))
            .OnDelete(DeleteBehavior.NoAction);

    }
}