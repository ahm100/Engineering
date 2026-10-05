using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Persistence.Configurations.RequestGoodsSupplies;

public class RequestGoodsSupplyProductConfiguration : IEntityTypeConfiguration<RequestGoodsSupplyProduct>
{
    private const string _tableName = "RequestGoodsSupplyProducts";
    public void Configure(EntityTypeBuilder<RequestGoodsSupplyProduct> builder)
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

        builder.Property(oo => oo.SerialNumber)
            .HasColumnType("nvarchar(250)")
            .IsRequired();

        builder.Ignore(oo => oo.RequestSerialNumber);

        builder.Property(oo => oo.CreatorId)
            .IsRequired();

        builder.Property(oo => oo.Importance);

        builder.Property(oo => oo.Status)
        .HasDefaultValue(GoodsSupplyDetailStatus.New)
        .IsRequired();

        builder.Property(oo => oo.ProductId)
            .IsRequired();

        builder.Property(oo => oo.ProductGroupId)
            .IsRequired();

        builder.Property(oo => oo.CheckGroup)
             .HasDefaultValue(false)
             .IsRequired();

        builder.Property(oo => oo.RequestedCount)
             .HasColumnType("decimal(18, 2)")
             .HasDefaultValue(null);

        builder.Property(oo => oo.DelivaryDeadLine);

        builder.Property(oo => oo.UnitPrice)
             .HasColumnType("decimal(18, 2)")
             .HasDefaultValue(null);

        builder.Property(oo => oo.PackingPrice)
             .HasColumnType("decimal(18, 2)")
             .HasDefaultValue(null);

        builder.Property(oo => oo.TransferPrice)
             .HasColumnType("decimal(18, 2)")
             .HasDefaultValue(null);

        builder.Property(oo => oo.TotalPrice)
             .HasColumnType("decimal(18, 2)")
             .HasDefaultValue(null);

        builder.Property(oo => oo.TaxPercentage)
             .HasColumnType("decimal(5, 2)")
             .HasDefaultValue(null);

        builder.Property(oo => oo.TaxNumber)
             .HasColumnType("decimal(18, 2)")
             .HasDefaultValue(null);

        builder.Property(oo => oo.DiscountByPercentage)
             .HasColumnType("decimal(5, 2)")
             .HasDefaultValue(null);

        builder.Property(oo => oo.DiscountByNumber)
             .HasColumnType("decimal(18, 2)")
             .HasDefaultValue(null);

        builder.Property(oo => oo.DiscountedPrice)
             .HasColumnType("decimal(18, 2)")
             .HasDefaultValue(null);

        builder.Property(oo => oo.FinalPrice)
             .HasColumnType("decimal(18, 2)")
             .HasDefaultValue(null);

        builder.Property(oo => oo.PackageCount)
             .HasColumnType("decimal(18, 2)")
             .HasDefaultValue(null);

        builder.Property(oo => oo.PackageUnitPrice)
             .HasColumnType("decimal(18, 2)")
             .HasDefaultValue(null);

        builder.Property(oo => oo.ContractorId);

        builder.Property(oo => oo.PackageId);

        builder.Property(oo => oo.DestinationWarehouseId);

        builder.Property(oo => oo.CustomerInvoiceNumber)
            .HasColumnType("nvarchar(50)");

        builder.Property(oo => oo.Description)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.ManagementDescription)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.LastDescription)
            .HasColumnType("nvarchar(1500)");

        builder.HasOne(oo => oo.RequestGoodsSupply)
               .WithMany(oo => oo.RequestGoodsSupplyProducts)
               .HasForeignKey("RequestGoodsSupplyId")
               .HasPrincipalKey(nameof(RequestGoodsSupply.Id))
               .OnDelete(DeleteBehavior.Restrict);

    }
}
