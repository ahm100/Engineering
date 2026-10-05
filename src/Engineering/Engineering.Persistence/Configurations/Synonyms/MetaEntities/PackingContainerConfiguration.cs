using Engineering.Domain.Entities.Synonyms.Warehouse.Packings;

namespace Engineering.Persistence.Configurations.Synonyms.MetaEntities;

public class PackingContainerConfiguration : IEntityTypeConfiguration<ViewPackingContainer>
{
    public void Configure(EntityTypeBuilder<ViewPackingContainer> builder)
    {
        builder.ToView("ViewPackingContainer", "engineer");

        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Packing)
            .WithMany()
            .HasForeignKey(x => x.PackingId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Ignore(e => e.PackingProducts);
        builder.Ignore(e => e.PackingPallets);
        //builder.HasMany(x => x.PackingProducts)
        //    .WithOne(x => x.PackingContainer)
        //    .HasForeignKey(x => x.PackingContainerId)
        //    .HasPrincipalKey(x => x.Id)
        //    .OnDelete(DeleteBehavior.Restrict);

        //builder.HasMany(x => x.PackingPallets)
        //    .WithOne(x => x.PackingContainer)
        //    .HasForeignKey(x => x.PackingContainerId)
        //    .HasPrincipalKey(x => x.Id)
        //    .OnDelete(DeleteBehavior.Restrict);
    }
}
