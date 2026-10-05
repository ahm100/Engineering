using Engineering.Domain.Entities.Synonyms.MetaData.Legals;

namespace Engineering.Persistence.Configurations.Synonyms.MetaEntities;

public class LegalConfiguration : IEntityTypeConfiguration<ViewLegal>
{
    public void Configure(EntityTypeBuilder<ViewLegal> builder)
    {
        builder.ToView("ViewLegal", "engineer");
    }
}
