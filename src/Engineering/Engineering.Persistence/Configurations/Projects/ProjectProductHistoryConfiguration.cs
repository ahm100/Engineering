using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.Projects.Histories;

namespace Engineering.Persistence.Configurations.Projects;
public class ProjectProductHistoryConfiguration : IEntityTypeConfiguration<ProjectProductHistory>
{
    private const string TableName = "ProjectProductHistories";
    public void Configure(EntityTypeBuilder<ProjectProductHistory> builder)
    {
        builder.MetaActiveConfiguration<ProjectProductHistory, long>(TableName);

        builder.Property(oo => oo.RequestQuantity)
            .HasDefaultValue(0)
            .IsRequired();

        builder.Property(oo => oo.RemainingQuantity)
            .HasDefaultValue(0)
            .IsRequired();

        builder.Property(oo => oo.CompletedQuantity)
            .HasDefaultValue(0)
            .IsRequired();

        builder.Property(oo => oo.InProgressQuantity)
            .HasDefaultValue(0)
            .IsRequired();

        builder.Property(oo => oo.DefaultManagerSet)
            .HasComment(ProjectCmts.DefaultManagerSet)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(oo => oo.IsActive)
            .HasDefaultValue(1);

        builder.Property(oo => oo.TolerancePercentage)
            .HasDefaultValue(0)
            .HasComment(ProjectCmts.TolerancePercentage)
            .HasColumnType("decimal(18,5)")
            .IsRequired();

        builder.Property(oo => oo.ProductGroupId)
            .HasComment(ProjectCmts.ProductGroupId);

        builder.Property(oo => oo.ProductCategoryId)
            .HasComment(ProjectCmts.ProductCategoryId);

        builder.HasOne(oo => oo.ProjectProduct)
            .WithMany(oo => oo.ProjectProductHistories)
            .HasForeignKey("ProjectProductId").HasPrincipalKey(nameof(Project.Id))
            .OnDelete(DeleteBehavior.NoAction);
    }
}