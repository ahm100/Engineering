using Engineering.Domain.Entities.Synonyms.MetaData.Cities;

namespace Engineering.Persistence.Configurations.Synonyms.MetaEntities;

public class CityConfiguration : IEntityTypeConfiguration<ViewCity>
{
    public void Configure(EntityTypeBuilder<ViewCity> builder)
    {
        builder.ToView("ViewCity", "engineer");

        builder.HasMany(x => x.Regions)
            .WithOne(x => x.City)
            .HasForeignKey(x => x.CityId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
