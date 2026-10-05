using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Persistence.Configurations.ContractorContracts;

public class ContractorContractDetailPriceHistoryConfiguration : IEntityTypeConfiguration<ContractorContractDetailPriceHistory>
{
    private const string TableName = "ContractorContractDetailPriceHistories";
    public void Configure(EntityTypeBuilder<ContractorContractDetailPriceHistory> builder)
    {
        builder.MetaActiveConfiguration<ContractorContractDetailPriceHistory, long>(TableName);

        builder.Property(oo => oo.StartDate)
            .IsRequired();

        builder.Property(oo => oo.EndDate)
            .IsRequired();

        builder.Property(oo => oo.Price)
            .HasColumnType("decimal(18, 2)")
            .IsRequired();

        builder.Property(oo => oo.CurrencyId)
            .IsRequired();

        builder.Property(oo => oo.ContractorContractDetailPriceId)
            .IsRequired();
        builder.HasOne(oo => oo.ContractorContractDetailPrice)
            .WithMany(oo => oo.ContractorContractDetailPriceHistories)
            .HasForeignKey("ContractorContractDetailPriceId")
            .HasPrincipalKey(nameof(ContractorContractDetailPrice.Id))
            .OnDelete(DeleteBehavior.Restrict);
    }
}
