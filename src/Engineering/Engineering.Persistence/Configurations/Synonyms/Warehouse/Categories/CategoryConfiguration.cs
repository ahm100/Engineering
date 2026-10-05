using Engineering.Domain.Entities.Synonyms.Warehouse.Categories;
namespace Engineering.Persistence.Configurations.Synonyms.Warehouse.Categories;

public class CategoryConfiguration : IEntityTypeConfiguration<ViewCategory>
{
    public void Configure(EntityTypeBuilder<ViewCategory> builder)
    {
        builder.ToView("ViewCategory", "engineer");
    }
}

