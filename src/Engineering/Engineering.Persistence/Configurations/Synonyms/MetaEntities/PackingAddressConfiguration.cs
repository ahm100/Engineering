using Engineering.Domain.Entities.Synonyms.Warehouse.Packings;

namespace Engineering.Persistence.Configurations.Synonyms.MetaEntities;

public class PackingAddressConfiguration : IEntityTypeConfiguration<ViewPackingAddress>
{
    public void Configure(EntityTypeBuilder<ViewPackingAddress> builder)
    {
        builder.ToView("ViewPackingAddress", "engineer");

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Warehouse)
            .WithMany()
            .HasForeignKey(x => x.WarehouseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Packing)
            .WithMany()
            .HasForeignKey(x => x.PackingId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Ignore(e => e.PackingProducts);
        //builder.HasMany(x => x.PackingProducts)
        //    .WithOne(x => x.DestinationPackingAddress)
        //    .HasForeignKey(x => x.DestinationPackingAddressId)
        //    .HasPrincipalKey(x => x.Id)
        //    .OnDelete(DeleteBehavior.Restrict);
    }
}
