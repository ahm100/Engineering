using Engineering.Domain.Entities.Synonyms.MetaData.BrandModels;

namespace Engineering.Persistence.Configurations.Synonyms.Meta.BrandModels;

public class BrandModelConfiguration : IEntityTypeConfiguration<ViewBrandModel>
{
    public void Configure(EntityTypeBuilder<ViewBrandModel> builder)
    {
        builder.ToView("ViewBrandModel", "engineer");
    }
}
