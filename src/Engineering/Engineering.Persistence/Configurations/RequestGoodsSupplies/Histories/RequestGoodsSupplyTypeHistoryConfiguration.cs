using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;
using Engineering.Domain.Entities.RequestGoodsSupplies.Histories;

namespace Engineering.Persistence.Configurations.RequestGoodsSupplies.Histories;

public class RequestGoodsSupplyTypeHistoryConfiguration : IEntityTypeConfiguration<RequestGoodsSupplyTypeHistory>
{
    private const string TableName = "RequestGoodsSupplyTypeHistories";
    public void Configure(EntityTypeBuilder<RequestGoodsSupplyTypeHistory> builder)
    {
        builder.MetaConfiguration<RequestGoodsSupplyTypeHistory, long>(TableName);

        builder.Property(oo => oo.SerialNumber)
            .HasColumnType("nvarchar(250)")
            .IsRequired();

        builder.Ignore(oo => oo.RequestSerialNumber);

        builder.Property(oo => oo.Importance);

        builder.Property(oo => oo.Status)
            .HasDefaultValue(RGSTypeStatus.Requested)
            .IsRequired();

        builder.Property(oo => oo.ReferenceId)
            .HasComment(RGSCmts.ReferenceId);

        builder.Property(oo => oo.Type)
            .HasComment(RGSCmts.SupplyType)
            .IsRequired();

        builder.Property(oo => oo.ProjectName)
            .HasComment(ProjectCmts.ProjectName)
            .HasColumnType("nvarchar(250)")
            .HasMaxLength(250);

        builder.Property(oo => oo.ProjectEnName)
            .HasComment(ProjectCmts.ProjectName)
            .HasColumnType("nvarchar(250)")
            .HasMaxLength(250);

        builder.Property(oo => oo.ProjectCode)
            .HasComment(ProjectCmts.ProjectCode)
            .HasColumnType("nvarchar(100)")
            .HasMaxLength(100);

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

        builder.HasOne(oo => oo.RequestGoodsSupplyType)
               .WithMany(oo => oo.RequestGoodsSupplyTypeHistories)
               .HasForeignKey("RequestGoodsSupplyTypeId")
               .HasPrincipalKey(nameof(RequestGoodsSupplyType.Id))
               .OnDelete(DeleteBehavior.Restrict);
    }
}
