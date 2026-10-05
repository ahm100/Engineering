using Engineering.Domain.Entities.Synonyms.Warehouse.Packings;

namespace Engineering.Persistence.Configurations.Synonyms.MetaEntities;

public class PackingConfiguration : IEntityTypeConfiguration<ViewPacking>
{
    public void Configure(EntityTypeBuilder<ViewPacking> builder)
    {
        builder.ToView("ViewPacking", "engineer");

        builder.HasKey(x => x.Id);
        builder.Ignore(e => e.PackingShippingDetails);
        builder.Ignore(e => e.PackingProducts);
        builder.Ignore(e => e.PackingAddress);
        builder.Ignore(e => e.PackingContainers);
        builder.Ignore(e => e.PackingPallets);
        //builder.HasMany(x => x.PackingProducts)
        //    .WithOne(x => x.Packing)
        //    .HasForeignKey(x => x.PackingId)
        //    .HasPrincipalKey(x => x.Id)
        //    .OnDelete(DeleteBehavior.Restrict);

        //builder.HasMany(x => x.PackingAddress)
        //    .WithOne(x => x.Packing)
        //    .HasForeignKey(x => x.PackingId)
        //    .HasPrincipalKey(x => x.Id)
        //    .OnDelete(DeleteBehavior.Restrict);

        //builder.HasMany(x => x.PackingContainers)
        //    .WithOne(x => x.Packing)
        //    .HasForeignKey(x => x.PackingId)
        //    .HasPrincipalKey(x => x.Id)
        //    .OnDelete(DeleteBehavior.Restrict);

        //builder.HasMany(x => x.PackingPallets)
        //    .WithOne(x => x.Packing)
        //    .HasForeignKey(x => x.PackingId)
        //    .HasPrincipalKey(x => x.Id)
        //    .OnDelete(DeleteBehavior.Restrict);
    }
}
