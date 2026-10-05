using Engineering.Domain.Entities.Synonyms.Warehouse.Packages;

namespace Engineering.Persistence.Configurations.Synonyms.MetaEntities;

public class PackageConfiguration : IEntityTypeConfiguration<ViewPackage>
{
    public void Configure(EntityTypeBuilder<ViewPackage> builder)
    {
        builder.ToView("ViewPackage", "engineer");
    }
}
