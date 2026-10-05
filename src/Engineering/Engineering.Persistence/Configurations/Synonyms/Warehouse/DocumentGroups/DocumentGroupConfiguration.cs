using Engineering.Domain.Entities.Synonyms.Warehouse.DocumentGroups;

namespace Engineering.Persistence.Configurations.Synonyms.Warehouse.DocumentGroups;

public class DocumentGroupConfiguration : IEntityTypeConfiguration<ViewDocumentGroup>
{
    public void Configure(EntityTypeBuilder<ViewDocumentGroup> builder)
    {
        builder.ToView("ViewDocumentGroup", "engineer");
    }
}
