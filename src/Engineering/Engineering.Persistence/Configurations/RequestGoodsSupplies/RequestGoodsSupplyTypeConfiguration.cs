using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Persistence.Configurations.RequestGoodsSupplies;

public class RequestGoodsSupplyTypeConfiguration : IEntityTypeConfiguration<RequestGoodsSupplyType>
{
    private const string TableName = "RequestGoodsSupplyTypes";
    public void Configure(EntityTypeBuilder<RequestGoodsSupplyType> builder)
    {
        builder.MetaConfiguration<RequestGoodsSupplyType, long>(TableName);

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

        builder.Property(oo => oo.CheckGroup)
             .HasDefaultValue(false)
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

        builder.Property(oo => oo.TotalPrice)
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

        builder.Property(oo => oo.Description)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.ManagementDescription)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.LastDescription)
            .HasColumnType("nvarchar(1500)");

        builder.HasOne(oo => oo.RequestGoodsSupply)
               .WithMany(oo => oo.RequestGoodsSupplyTypes)
               .HasForeignKey("RequestGoodsSupplyId")
               .HasPrincipalKey(nameof(RequestGoodsSupply.Id))
               .OnDelete(DeleteBehavior.Restrict);
    }
}
