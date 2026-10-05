using Engineering.Domain.Entities.FiduciaryProducts;
using Engineering.Domain.Entities.FiduciaryProducts.Enums;
using Engineering.Domain.Entities.ProjectOperations;
using Engineering.Domain.Entities.Projects;

namespace Engineering.Persistence.Configurations.FiduciaryProducts;

public class FiduciaryProductConfiguration : IEntityTypeConfiguration<FiduciaryProduct>
{
    private const string _tableName = "FiduciaryProducts";
    public void Configure(EntityTypeBuilder<FiduciaryProduct> builder)
    {
        builder.ToTable(_tableName);

        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(oo => oo.Created)
            .ValueGeneratedOnAdd().
            IsRequired();

        builder.Property(oo => oo.RequestNumber);

        builder.Property(oo => oo.CreatorId)
            .IsRequired();

        builder.Property(oo => oo.CompanyId);

        builder.Property(oo => oo.ThirdPartyId)
            .IsRequired();

        builder.Property(oo => oo.Description)
            .HasColumnType("nvarchar(1500)")
            .HasDefaultValue(null);

        builder.Property(oo => oo.LastDescription)
            .HasColumnType("nvarchar(1500)")
            .HasDefaultValue(null);

        builder.Property(oo => oo.StatusDescription)
            .HasColumnType("nvarchar(1500)")
            .HasDefaultValue(null);

        builder.Property(oo => oo.Status)
            .HasDefaultValue(FiduciaryProductStatus.New)
            .IsRequired();


        builder.HasOne(oo => oo.Project)
               .WithMany(oo => oo.FiduciaryProducts)
               .HasForeignKey("ProjectId")
               .HasPrincipalKey(nameof(Project.Id));

        builder.HasOne(oo => oo.ProjectOperation)
               .WithMany(oo => oo.FiduciaryProducts)
               .HasForeignKey("ProjectOperationId")
               .HasPrincipalKey(nameof(ProjectOperation.Id));

    }
}
