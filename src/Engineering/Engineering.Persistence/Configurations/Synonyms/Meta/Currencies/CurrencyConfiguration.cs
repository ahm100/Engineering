using Engineering.Domain.Entities.Synonyms.MetaData.Currencies;

namespace Engineering.Persistence.Configurations.Synonyms.Meta.Currencies;
public class ViewCurrencyConfiguration : IEntityTypeConfiguration<ViewCurrency>
{
    public void Configure(EntityTypeBuilder<ViewCurrency> builder)
    {
        builder.ToView("ViewCurrency", "engineer");
    }
}