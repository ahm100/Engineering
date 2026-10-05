using Engineering.Domain.Entities.Synonyms.MetaData.ThirdParties;

namespace Engineering.Persistence.Configurations.Synonyms.MetaEntities;

public class ThirdPartyConfiguration : IEntityTypeConfiguration<ViewThirdParty>
{
    public void Configure(EntityTypeBuilder<ViewThirdParty> builder)
    {
        builder.ToView("ViewThirdParty", "engineer");

        builder.HasOne(x => x.Legal)
       .WithMany()
       .HasForeignKey(x => x.LegalId)
       .OnDelete(DeleteBehavior.Restrict);
    }
}
