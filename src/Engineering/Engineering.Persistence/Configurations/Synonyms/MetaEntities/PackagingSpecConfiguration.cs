using Engineering.Domain.Entities.Synonyms.Warehouse.Packages;

namespace Engineering.Persistence.Configurations.Synonyms.MetaEntities;

public class PackagingSpecConfiguration : IEntityTypeConfiguration<ViewPackagingSpec>
{
    public void Configure(EntityTypeBuilder<ViewPackagingSpec> builder)
    {
        builder.ToView("ViewPackagingSpec", "engineer");

        builder.HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
