using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Persistence.Configurations.ContractorContracts;

public class ContractorContractDetailPriceConfiguration : IEntityTypeConfiguration<ContractorContractDetailPrice>
{
    private const string TableName = "ContractorContractDetailPrices";
    public void Configure(EntityTypeBuilder<ContractorContractDetailPrice> builder)
    {
        builder.MetaActiveConfiguration<ContractorContractDetailPrice, long>(TableName);

        builder.Property(oo => oo.ContractorContractDetailId)
            .IsRequired();

        builder.Property(oo => oo.StartDate)
            .IsRequired();

        builder.Property(oo => oo.EndDate)
            .IsRequired();

        builder.Property(oo => oo.Price)
            .HasColumnType("decimal(18, 2)")
            .IsRequired();

        builder.Property(oo => oo.CurrencyId)
            .IsRequired();

        builder.HasOne(oo => oo.ContractorContractDetail)
            .WithMany(oo => oo.ContractorContractDetailPrices)
            .HasForeignKey("ContractorContractDetailId")
            .HasPrincipalKey(nameof(ContractorContractDetail.Id))
            .OnDelete(DeleteBehavior.Restrict);
    }
}
