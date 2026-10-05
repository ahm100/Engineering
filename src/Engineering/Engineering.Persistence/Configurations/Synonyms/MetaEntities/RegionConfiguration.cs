using Engineering.Domain.Entities.Synonyms.MetaData.Regions;

namespace Engineering.Persistence.Configurations.Synonyms.MetaEntities;

public class RegionConfiguration : IEntityTypeConfiguration<ViewRegion>
{
    public void Configure(EntityTypeBuilder<ViewRegion> builder)
    {
        builder.ToView("ViewRegion", "engineer");

        builder.HasOne(x => x.City)
            .WithMany(x => x.Regions)
            .HasForeignKey(x => x.CityId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
