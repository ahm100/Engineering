using Engineering.Domain.Entities.Synonyms.Warehouse.Warehouses;

namespace Engineering.Persistence.Configurations.Synonyms.MetaEntities;

public class WarehouseConfiguration : IEntityTypeConfiguration<ViewWarehouse>
{
    public void Configure(EntityTypeBuilder<ViewWarehouse> builder)
    {
        builder.ToView("ViewWarehouse", "engineer");

        builder.HasOne(x => x.Address)
       .WithMany()
       .HasForeignKey(x => x.AddressId)
       .OnDelete(DeleteBehavior.Restrict);
    }
}
