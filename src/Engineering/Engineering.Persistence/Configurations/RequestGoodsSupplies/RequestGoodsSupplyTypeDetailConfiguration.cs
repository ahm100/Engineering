using Engineering.Domain.Entities.CostCenters;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Persistence.Configurations.RequestGoodsSupplies;

public class RequestGoodsSupplyTypeDetailConfiguration : IEntityTypeConfiguration<RequestGoodsSupplyTypeDetail>
{
    private const string TableName = "RequestGoodsSupplyTypeDetails";
    public void Configure(EntityTypeBuilder<RequestGoodsSupplyTypeDetail> builder)
    {
        builder.MetaConfiguration<RequestGoodsSupplyTypeDetail, long>(TableName);

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

        builder.Property(oo => oo.DescriptionEn)
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

        builder.HasOne(oo => oo.RequestGoodsSupply)
               .WithMany(oo => oo.RequestGoodsSupplyTypeDetails)
               .HasForeignKey("RequestGoodsSupplyId")
               .HasPrincipalKey(nameof(RequestGoodsSupply.Id))
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(oo => oo.RequestGoodsSupplyType)
               .WithMany(oo => oo.RequestGoodsSupplyTypeDetails)
               .HasForeignKey("RequestGoodsSupplyTypeId")
               .HasPrincipalKey(nameof(RequestGoodsSupplyType.Id))
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(oo => oo.ProjectProduct)
               .WithMany(oo => oo.RequestGoodsSupplyTypeDetails)
               .HasForeignKey("ProjectProductId")
               .HasPrincipalKey(nameof(ProjectProduct.Id))
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(oo => oo.CostCenter)
            .WithMany(oo => oo.RequestGoodsSupplyTypeDetails)
            .HasForeignKey("CostCenterId").HasPrincipalKey(nameof(CostCenter.Id))
            .OnDelete(DeleteBehavior.NoAction);
    }
}
