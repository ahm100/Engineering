using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Persistence.Configurations.Projects;

public class ProjectProductConfiguration : IEntityTypeConfiguration<ProjectProduct>
{
    private const string TableName = "ProjectProducts";
    public void Configure(EntityTypeBuilder<ProjectProduct> builder)
    {
        builder.MetaActiveConfiguration<ProjectProduct, long>(TableName);

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

        builder.Property(oo => oo.ProjectProductType)
            .HasComment(ProjectCmts.ProjectProductType)
            .HasDefaultValue(ProjectProductType.ProductGroup)
            .IsRequired();

        builder.HasOne(oo => oo.Project)
            .WithMany(oo => oo.ProjectProducts)
            .HasForeignKey("ProjectId").HasPrincipalKey(nameof(Project.Id))
            .OnDelete(DeleteBehavior.NoAction);

        builder.Property(oo => oo.CompanyId);
    }
}