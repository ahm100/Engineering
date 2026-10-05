using Engineering.Domain.Entities.Synonyms.Warehouse.Groups;
namespace Engineering.Persistence.Configurations.Synonyms.Warehouse.Groups;

public class GroupConfiguration : IEntityTypeConfiguration<ViewGroup>
{
    public void Configure(EntityTypeBuilder<ViewGroup> builder)
    {
        builder.ToView("ViewGroup", "engineer");
    }
}
