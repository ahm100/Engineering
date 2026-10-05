using Engineering.Domain.Entities.ProjectOperations;
using Engineering.Domain.Entities.Projects.WBS;

namespace Engineering.Persistence.Configurations.Projects.WBS;

public class ProjectOperationWbsConfiguration : IEntityTypeConfiguration<ProjectOperationWbs>
{
    private const string TableName = "ProjectOperationWbses";
    public void Configure(EntityTypeBuilder<ProjectOperationWbs> builder)
    {
        builder.MetaConfiguration<ProjectOperationWbs, long>(TableName);

        builder.HasOne(oo => oo.ProjectOperation)
            .WithMany(oo => oo.ProjectOperationWbses)
            .HasForeignKey("ProjectOperationId").HasPrincipalKey(nameof(ProjectOperation.Id))
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(oo => oo.ProjectWbs)
            .WithMany(oo => oo.ProjectOperationWbses)
            .HasForeignKey("ProjectWbsId").HasPrincipalKey(nameof(ProjectWbs.Id))
            .OnDelete(DeleteBehavior.NoAction);
    }
}