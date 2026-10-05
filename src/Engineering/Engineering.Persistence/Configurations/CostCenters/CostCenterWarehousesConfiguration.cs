using Engineering.Domain.Entities.CostCenters;

namespace Engineering.Persistence.Configurations.CostCenters;

public class CostCenterWarehousesConfiguration : IEntityTypeConfiguration<CostCenterWarehouse>
{
    private const string TableName = "CostCenterWarehouses";
    public void Configure(EntityTypeBuilder<CostCenterWarehouse> builder)
    {
        builder.MetaConfiguration<CostCenterWarehouse, long>(TableName);

        builder.Property(oo => oo.WarehouseId)
            .HasComment(CCenterCmts.WarehouseId)
            .IsRequired();

        builder.Property(oo => oo.IsDefault)
            .HasComment(CCenterCmts.IsDefault)
            .HasDefaultValue(false)
            .IsRequired();

        builder.HasOne(oo => oo.CostCenter)
            .WithMany(oo => oo.CostCenterWarehouses)
            .HasForeignKey("CostCenterId").HasPrincipalKey(nameof(CostCenter.Id))
            .OnDelete(DeleteBehavior.NoAction);

    }
}