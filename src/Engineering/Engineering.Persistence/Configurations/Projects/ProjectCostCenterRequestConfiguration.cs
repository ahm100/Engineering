using Engineering.Domain.Entities.Projects.ProjectCostCenterRequests;

namespace Engineering.Persistence.Configurations.Projects;

public class ProjectCostCenterRequestConfiguration : IEntityTypeConfiguration<ProjectCostCenterRequest>
{
    private const string TableName = "ProjectCostCenterRequests";
    public void Configure(EntityTypeBuilder<ProjectCostCenterRequest> builder)
    {
        builder.MetaConfiguration<ProjectCostCenterRequest, long>(TableName);
        builder.Property(x => x.RequestedCostCenterName)
            .HasComment(GlobalCmts.RequestedCostCenterName)
            .HasMaxLength(250)
            .IsUnicode(true)
            .IsRequired(false);

        builder.Property(x => x.Description)
            .HasComment(GlobalCmts.Description)
            .HasMaxLength(1500)
            .IsUnicode(true)
            .IsRequired();

        builder.Property(x => x.RejectionReason)
            .HasComment(GlobalCmts.RejectionReason)
            .HasMaxLength(1000)
            .IsUnicode(true);

        builder.Property(x => x.Status)
            .HasComment(GlobalCmts.Status)
            .IsRequired();

        builder.Property(x => x.CostCenterId)
            .HasComment(GlobalCmts.CostCenterId);

        builder.HasOne(x => x.Project)
            .WithMany()
            .HasForeignKey(x => x.ProjectId)
            .OnDelete(DeleteBehavior.Restrict); // Prevents cascade delete if the project is removed

        builder.HasOne(x => x.CostCenter)
            .WithMany()
            .HasForeignKey(x => x.CostCenterId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false); // Explicitly marks the relationship as optional/nullable
    }
}