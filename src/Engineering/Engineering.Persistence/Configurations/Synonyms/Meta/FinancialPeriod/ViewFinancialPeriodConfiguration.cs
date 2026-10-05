using Engineering.Domain.Entities.Synonyms.MetaData.FinancialPeriod;

namespace Engineering.Persistence.Configurations.Synonyms.Meta.FinancialPeriod;

public class ViewFinancialPeriodConfiguration : IEntityTypeConfiguration<ViewFinancialPeriod>
{
    public void Configure(EntityTypeBuilder<ViewFinancialPeriod> builder)
    {
        builder.ToView("ViewFinancialPeriod", "engineer");
    }
}