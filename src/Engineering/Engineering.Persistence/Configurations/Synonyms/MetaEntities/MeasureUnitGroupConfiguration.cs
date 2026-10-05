using Engineering.Domain.Entities.Synonyms.MetaData.MeasureUnits;

namespace Engineering.Persistence.Configurations.Synonyms.MetaEntities;

public class MeasureUnitGroupConfiguration : IEntityTypeConfiguration<MeasureUnitGroup>
{
    public void Configure(EntityTypeBuilder<MeasureUnitGroup> builder)
    {
        builder.ToView("MeasureUnitGroup", "engineer");
    }
}