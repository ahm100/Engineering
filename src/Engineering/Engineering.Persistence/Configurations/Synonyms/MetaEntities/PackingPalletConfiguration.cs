using Engineering.Domain.Entities.Synonyms.Warehouse.Packings;

namespace Engineering.Persistence.Configurations.Synonyms.MetaEntities;

public class PackingPalletConfiguration : IEntityTypeConfiguration<ViewPackingPallet>
{
    public void Configure(EntityTypeBuilder<ViewPackingPallet> builder)
    {
        builder.ToView("ViewPackingPallet", "engineer");

        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Packing)
            .WithMany()
            .HasForeignKey(x => x.PackingId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.PackagingSpec)
            .WithMany()
            .HasForeignKey(x => x.PackagingSpecId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.PackingContainer)
            .WithMany()
            .HasForeignKey(x => x.PackingContainerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Ignore(e => e.PackingProducts);
        //builder.HasMany(x => x.PackingProducts)
        //    .WithOne(x => x.PackingPallet)
        //    .HasForeignKey(x => x.PackingPalletId)
        //    .HasPrincipalKey(x => x.Id)
        //    .OnDelete(DeleteBehavior.Restrict);
    }
}
