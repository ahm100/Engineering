using Engineering.Domain.Entities.CostCenters;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.Projects.Junctions;

namespace Engineering.Persistence.Configurations.Projects;

public class ProjectCostCenterConfiguration : IEntityTypeConfiguration<ProjectCostCenter>
{
    private const string TableName = "ProjectCostCenters";
    public void Configure(EntityTypeBuilder<ProjectCostCenter> builder)
    {

        builder.MetaConfiguration<ProjectCostCenter, long>(TableName);

        builder.Property(oo => oo.IsDefault)
            .HasDefaultValue(false)
            .IsRequired();

        builder.HasOne(oo => oo.Project)
            .WithMany(oo => oo.ProjectCostCenters)
            .HasForeignKey("ProjectId")
            .HasPrincipalKey(nameof(Project.Id))
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(oo => oo.CostCenter)
            .WithMany(oo => oo.ProjectCostCenters)
            .HasForeignKey("CostCenterId")
            .HasPrincipalKey(nameof(CostCenter.Id))
            .OnDelete(DeleteBehavior.NoAction);
    }
}