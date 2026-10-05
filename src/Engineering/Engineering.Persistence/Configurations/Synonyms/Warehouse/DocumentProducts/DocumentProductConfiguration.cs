using Engineering.Domain.Entities.Synonyms.Warehouse.DocumentProducts;

namespace Engineering.Persistence.Configurations.Synonyms.Warehouse.DocumentProducts;

public class DocumentProductConfiguration : IEntityTypeConfiguration<ViewDocumentProduct>
{
    public void Configure(EntityTypeBuilder<ViewDocumentProduct> builder)
    {
        builder.ToView("ViewDocumentProduct", "engineer");
    }
}
