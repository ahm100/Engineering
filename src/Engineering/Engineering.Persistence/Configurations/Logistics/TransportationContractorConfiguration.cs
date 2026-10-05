using Engineering.Domain.Entities.Logistics;

namespace Engineering.Persistence.Configurations.Logistics;

public class TransportationContractorConfiguration : IEntityTypeConfiguration<TransportationContractor>
{
    private const string _tableName = "TransportationContractors";
    public void Configure(EntityTypeBuilder<TransportationContractor> builder)
    {
        builder.MetaActiveConfiguration<TransportationContractor, long>(_tableName);

        builder.Property(a => a.LegacyId)
            .HasComment(GlobalCmts.LegacyId);

        builder.Property(a => a.StartOfContract)
            .HasComment(TransportationContractorCmts.StartOfContract)
            .IsRequired(false);

        builder.Property(a => a.Title)
            .HasComment(TransportationContractorCmts.Title)
            .IsRequired(false);

        builder.Property(a => a.EndOfContract)
            .HasComment(TransportationContractorCmts.EndOfContract)
            .IsRequired(false);

        builder.Property(a => a.Type)
            .HasComment(TransportationContractorCmts.Type)
            .IsRequired();

        builder.Property(a => a.PercentageValue)
            .HasColumnType("decimal(18,2)")
            .HasComment(TransportationContractorCmts.PercentageValue)
            .IsRequired(false);

        builder.Property(a => a.FixedNumber)
            .HasColumnType("decimal(18,2)")
            .HasComment(TransportationContractorCmts.FixedNumber)
            .IsRequired(false);

        builder.Property(a => a.FirstPrefix)
            .HasColumnType("nvarchar(10)")
            .HasComment(TransportationContractorCmts.FirstPrefix)
            .IsRequired(true);

        builder.Property(a => a.SecondPrefix)
            .HasColumnType("bigint")
            .HasComment(TransportationContractorCmts.SecondPrefix)
            .IsRequired(false);

        builder.Property(a => a.TaxPercent)
            .HasColumnType("decimal(18,2)")
            .HasComment(TransportationContractorCmts.TaxPercent)
            .IsRequired(false);

        builder.Property(a => a.ServicePrice)
            .HasColumnType("decimal(18,2)")
            .HasComment(TransportationContractorCmts.ServicePrice)
            .IsRequired(false);

        builder.Property(oo => oo.CompanyId)
            .HasComment(GlobalCmts.CompanyId)
            .IsRequired();

        builder.HasOne(x => x.ThirdParty)
            .WithMany()
            .HasForeignKey(x => x.ThirdPartyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
