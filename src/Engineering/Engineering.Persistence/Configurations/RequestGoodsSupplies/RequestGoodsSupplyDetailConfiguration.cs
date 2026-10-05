using Engineering.Domain.Entities.CostCenters;
using Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Persistence.Configurations.RequestGoodsSupplies;

public class RequestGoodsSupplyDetailConfiguration : IEntityTypeConfiguration<RequestGoodsSupplyDetail>
{
    private const string _tableName = "RequestGoodsSupplyDetails";
    public void Configure(EntityTypeBuilder<RequestGoodsSupplyDetail> builder)
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

        builder.Property(oo => oo.ContractorId);

        builder.Property(oo => oo.PackageId);

        builder.Property(oo => oo.PackageCount);

        builder.Property(oo => oo.DestinationWarehouseId);

        builder.Property(oo => oo.CheckGroup)
             .HasDefaultValue(false)
             .IsRequired();

        builder.Property(oo => oo.Status)
        .HasDefaultValue(GoodsSupplyDetailStatus.New)
        .IsRequired();

        builder.Property(oo => oo.Description)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.ManagementDescription)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.Importance);

        builder.Property(oo => oo.DelivaryDeadLine);

        builder.Property(oo => oo.RequestedCount)
            .HasColumnType("decimal(18,2)")
             .IsRequired();

        builder.Property(oo => oo.UnitPrice)
             .HasColumnType("decimal(18, 2)")
             .HasDefaultValue(null);

        builder.Property(oo => oo.TotalPrice)
             .HasColumnType("decimal(18, 2)")
             .HasDefaultValue(null);

        builder.Property(oo => oo.DiscountByNumber)
             .HasColumnType("decimal(18, 2)")
             .HasDefaultValue(null);

        builder.Property(oo => oo.DiscountByPercentage)
             .HasColumnType("decimal(5, 2)")
             .HasDefaultValue(null);

        builder.Property(oo => oo.DiscountedPrice)
             .HasColumnType("decimal(18, 2)")
             .HasDefaultValue(null);

        builder.Property(oo => oo.TaxNumber)
             .HasColumnType("decimal(18, 2)")
             .HasDefaultValue(null);

        builder.Property(oo => oo.TaxPercentage)
             .HasColumnType("decimal(5, 2)")
             .HasDefaultValue(null);

        builder.Property(oo => oo.PackingPrice)
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

        builder.Property(oo => oo.CustomerInvoiceNumber)
            .HasColumnType("nvarchar(50)");

        builder.Property(oo => oo.LastDescription)
            .HasColumnType("nvarchar(1500)");

        builder.HasOne(oo => oo.RequestGoodsSupply)
               .WithMany(oo => oo.RequestGoodsSupplyDetails)
               .HasForeignKey("RequestGoodsSupplyId")
               .HasPrincipalKey(nameof(RequestGoodsSupply.Id))
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(oo => oo.RequestGoodsSupplyProduct)
               .WithMany(oo => oo.RequestGoodsSupplyDetails)
               .HasForeignKey("RequestGoodsSupplyProductId")
               .HasPrincipalKey(nameof(RequestGoodsSupplyProduct.Id))
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(oo => oo.ConsumableVolumeProduct)
               .WithMany(oo => oo.RequestGoodsSupplyDetails)
               .HasForeignKey("ConsumableVolumeProductId")
               .HasPrincipalKey(nameof(ConsumableVolumeProduct.Id))
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(oo => oo.ProjectProduct)
               .WithMany(oo => oo.RequestGoodsSupplyDetails)
               .HasForeignKey("ProjectProductId")
               .HasPrincipalKey(nameof(ProjectProduct.Id))
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(oo => oo.CostCenter)
            .WithMany(oo => oo.RequestGoodsSupplyDetails)
            .HasForeignKey("CostCenterId").HasPrincipalKey(nameof(CostCenter.Id))
            .OnDelete(DeleteBehavior.NoAction);
    }
}
