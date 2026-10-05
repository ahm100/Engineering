using Engineering.Domain.Entities.Synonyms.Warehouse.WarehouseAssets;

namespace Engineering.Persistence.Configurations.Synonyms.Warehouse.WarehouseAssets;

public class WarehouseAssetConfiguration : IEntityTypeConfiguration<ViewWarehouseAsset>
{
    public void Configure(EntityTypeBuilder<ViewWarehouseAsset> builder)
    {
        builder.ToView("ViewWarehouseAsset", "engineer");
    }
}