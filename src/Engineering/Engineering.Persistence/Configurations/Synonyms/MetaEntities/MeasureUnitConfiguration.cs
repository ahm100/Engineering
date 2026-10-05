using Engineering.Domain.Entities.Synonyms.MetaData.MeasureUnits;

namespace Engineering.Persistence.Configurations.Synonyms.MetaEntities;

public class MeasureUnitConfiguration : IEntityTypeConfiguration<MeasureUnit>
{
    public void Configure(EntityTypeBuilder<MeasureUnit> builder)
    {
        builder.ToView("MeasureUnit", "engineer");
    }
}