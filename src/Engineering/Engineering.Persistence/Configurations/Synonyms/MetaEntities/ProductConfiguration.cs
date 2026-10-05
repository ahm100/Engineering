using Engineering.Domain.Entities.Synonyms.Warehouse.Products;

namespace Engineering.Persistence.Configurations.Synonyms.MetaEntities;

public class ProductConfiguration : IEntityTypeConfiguration<ViewProduct>
{
    public void Configure(EntityTypeBuilder<ViewProduct> builder)
    {
        builder.ToView("ViewProduct", "engineer");
    }
}
