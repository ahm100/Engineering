using Engineering.Domain.Entities.Synonyms.MetaData.Organizations;

namespace Engineering.Persistence.Configurations.Synonyms.Meta.Organizations;

public class ViewOrganizationConfiguration : IEntityTypeConfiguration<ViewOrganization>
{
    public void Configure(EntityTypeBuilder<ViewOrganization> builder)
    {
        builder.ToView("ViewOrganization", "engineer");

        builder.HasKey(x => x.Id);
    }
}