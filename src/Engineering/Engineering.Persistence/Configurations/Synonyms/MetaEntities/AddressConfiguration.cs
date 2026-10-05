using Engineering.Domain.Entities.Synonyms.MetaData.Addresses;

namespace Engineering.Persistence.Configurations.Synonyms.MetaEntities;

public class AddressConfiguration : IEntityTypeConfiguration<ViewAddress>
{
    public void Configure(EntityTypeBuilder<ViewAddress> builder)
    {
        builder.ToView("ViewAddress", "engineer");

        builder.HasOne(x => x.City)
       .WithMany()
       .HasForeignKey(x => x.CityId)
       .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.ThirdParty)
       .WithMany(x => x.Addresses)
       .HasPrincipalKey(x => x.Id)
       .HasForeignKey(x => x.ThirdPartyId)
       .OnDelete(DeleteBehavior.Restrict);
    }
}
