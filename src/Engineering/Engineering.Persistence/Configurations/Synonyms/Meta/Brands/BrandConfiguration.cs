using Engineering.Domain.Entities.Synonyms.MetaData.Brands;

namespace Engineering.Persistence.Configurations.Synonyms.Meta.Brands;

public class BrandConfiguration : IEntityTypeConfiguration<ViewBrand>
{
    public void Configure(EntityTypeBuilder<ViewBrand> builder)
    {
        builder.ToView("ViewBrand", "engineer");
    }
}
