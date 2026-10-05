using Engineering.Domain.Entities.ProjectOperations;

namespace Engineering.Persistence.Configurations.ProjectOperations;

public class ProjectOperationDependencyConfiguration : IEntityTypeConfiguration<ProjectOperationDependency>
{
    private const string TableName = "ProjectOperationDependencies";
    public void Configure(EntityTypeBuilder<ProjectOperationDependency> builder)
    {
        builder.MetaConfiguration<ProjectOperationDependency, long>(TableName);

        builder.Property(oo => oo.LagDays)
            .HasDefaultValue(0)
            .IsRequired();

        builder.Property(oo => oo.DependencyType)
            .IsRequired();

        builder.HasOne(oo => oo.Predecessor)
            .WithMany(oo => oo.PredecessorProjectOperationDependencies)
            .HasForeignKey("PredecessorId").HasPrincipalKey(nameof(ProjectOperation.Id))
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(oo => oo.Successor)
            .WithMany(oo => oo.SuccessorProjectOperationDependencies)
            .HasForeignKey("SuccessorId").HasPrincipalKey(nameof(ProjectOperation.Id))
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

    }
}