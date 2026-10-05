using Engineering.Domain.Entities.FiduciaryProducts;
using Engineering.Domain.Entities.FiduciaryProducts.Enums;

namespace Engineering.Persistence.Configurations.FiduciaryProducts;

public class FiduciaryProductDetailManagementConfiguration : IEntityTypeConfiguration<FiduciaryProductDetailManagement>
{
    private const string _tableName = "FiduciaryProductDetailManagements";
    public void Configure(EntityTypeBuilder<FiduciaryProductDetailManagement> builder)
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
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(oo => oo.CreatorId)
            .IsRequired();

        builder.Property(oo => oo.WarehouseId)
            .IsRequired();

        builder.Property(oo => oo.DestinationWarehouseId);

        builder.Property(oo => oo.InvoiceId)
            .IsRequired();

        builder.Property(oo => oo.ConfirmedLoanCount)
            .IsRequired();

        builder.Property(oo => oo.Status)
            .HasDefaultValue(FiduciaryProductDetailManagementStatus.Pending)
            .IsRequired();

        builder.Property(oo => oo.Description)
            .HasColumnType("nvarchar(1500)")
            .HasDefaultValue(null);


        builder.HasOne(oo => oo.FiduciaryProductDetail)
               .WithMany(oo => oo.Managements)
               .HasForeignKey("FiduciaryProductDetailId")
               .HasPrincipalKey(nameof(FiduciaryProductDetail.Id))
               .OnDelete(DeleteBehavior.NoAction);
    }
}