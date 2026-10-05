using Engineering.Domain.Entities.Synonyms.Warehouse.Packings;

namespace Engineering.Persistence.Configurations.Synonyms.MetaEntities;

public class PackingShippingDetailConfiguration : IEntityTypeConfiguration<ViewPackingShippingDetail>
{
    public void Configure(EntityTypeBuilder<ViewPackingShippingDetail> builder)
    {
        builder.ToView("ViewPackingShippingDetail", "engineer");

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Packing)
            .WithMany()
            .HasForeignKey(x => x.PackingId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
