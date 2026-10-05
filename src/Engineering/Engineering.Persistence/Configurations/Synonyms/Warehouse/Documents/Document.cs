using Engineering.Domain.Entities.Synonyms.Warehouse.Documents;
namespace Engineering.Persistence.Configurations.Synonyms.Warehouse.Documents;

public class DocumentConfiguration : IEntityTypeConfiguration<ViewDocument>
{
    public void Configure(EntityTypeBuilder<ViewDocument> builder)
    {
        builder.ToView("ViewDocument", "engineer");
    }
}

