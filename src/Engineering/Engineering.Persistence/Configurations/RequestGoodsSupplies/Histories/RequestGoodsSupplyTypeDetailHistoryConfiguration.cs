using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;
using Engineering.Domain.Entities.RequestGoodsSupplies.Histories;

namespace Engineering.Persistence.Configurations.RequestGoodsSupplies.Histories;

public class RequestGoodsSupplyTypeDetailHistoryConfiguration : IEntityTypeConfiguration<RequestGoodsSupplyTypeDetailHistory>
{
    private const string TableName = "RequestGoodsSupplyTypeDetailHistories";
    public void Configure(EntityTypeBuilder<RequestGoodsSupplyTypeDetailHistory> builder)
    {
        builder.MetaConfiguration<RequestGoodsSupplyTypeDetailHistory, long>(TableName);

        builder.Property(oo => oo.ContractorId);

        builder.Property(oo => oo.PackageId);

        builder.Property(oo => oo.PackageCount);

        builder.Property(oo => oo.CheckGroup)
             .HasDefaultValue(false)
             .IsRequired();

        builder.Property(oo => oo.ReferenceId)
            .HasComment(RGSCmts.ReferenceId);

        builder.Property(oo => oo.Type)
            .HasComment(RGSCmts.SupplyType)
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

        builder.Property(oo => oo.LastDescription)
            .HasColumnType("nvarchar(1500)");

        builder.HasOne(oo => oo.RequestGoodsSupplyTypeDetail)
               .WithMany(oo => oo.RequestGoodsSupplyTypeDetailHistories)
               .HasForeignKey("RequestGoodsSupplyTypeDetailId")
               .HasPrincipalKey(nameof(RequestGoodsSupplyTypeDetail.Id))
               .OnDelete(DeleteBehavior.NoAction);
    }
}
