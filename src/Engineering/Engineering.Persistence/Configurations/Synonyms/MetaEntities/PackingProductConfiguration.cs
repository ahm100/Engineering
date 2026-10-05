using Engineering.Domain.Entities.Synonyms.Warehouse.Packings;

namespace Engineering.Persistence.Configurations.Synonyms.MetaEntities;

public class PackingProductConfiguration : IEntityTypeConfiguration<ViewPackingProduct>
{
    public void Configure(EntityTypeBuilder<ViewPackingProduct> builder)
    {
        builder.ToView("ViewPackingProduct", "engineer");

        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Package)
            .WithMany()
            .HasForeignKey(x => x.PackageId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Packing)
            .WithMany()
            .HasForeignKey(x => x.PackingId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.SourcePackingAddress)
            .WithMany()
            .HasForeignKey(x => x.SourcePackingAddressId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.DestinationPackingAddress)
            .WithMany()
            .HasForeignKey(x => x.DestinationPackingAddressId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.PackingContainer)
            .WithMany()
            .HasForeignKey(x => x.PackingContainerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.PackingPallet)
            .WithMany()
            .HasForeignKey(x => x.PackingPalletId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
