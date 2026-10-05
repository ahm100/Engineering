using Engineering.Domain.Entities.ProjectOperations;

namespace Engineering.Persistence.Configurations.ProjectOperations;

public class ProjectOperationActionConfiguration : IEntityTypeConfiguration<ProjectOperationAction>
{
    private const string TableName = "ProjectOperationActions";
    public void Configure(EntityTypeBuilder<ProjectOperationAction> builder)
    {
        builder.MetaConfiguration<ProjectOperationAction, long>(TableName);

        builder.Property(oo => oo.Price)
            .HasComment(ProjectOperationCmts.Price)
            .HasDefaultValue(0m)
            .HasColumnType("decimal(18,2)");

        builder.HasOne(a => a.OperationInfoAction)
            .WithMany(a => a.ProjectOperationActions)
            .HasForeignKey("OperationInfoActionId").HasPrincipalKey(o => o.Id)
            .OnDelete(DeleteBehavior.NoAction);
    }
}