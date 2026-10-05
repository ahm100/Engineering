using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Persistence.Configurations.ContractorContracts;

public class ContractorContractHistoryConfiguration : IEntityTypeConfiguration<ContractorContractHistory>
{
    private const string TableName = "ContractorContractHistories";
    public void Configure(EntityTypeBuilder<ContractorContractHistory> builder)
    {
        builder.MetaConfiguration<ContractorContractHistory, long>(TableName);


        builder.Property(oo => oo.DailyBaseHours)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.MonthlyBaseHours)
            .HasColumnType("decimal(18,2)");

        builder.HasOne(oo => oo.ContractorContract)
            .WithMany(oo => oo.Histories)
            .HasForeignKey("ContractorContractId")
            .HasPrincipalKey(nameof(ContractorContract.Id))
            .OnDelete(DeleteBehavior.Restrict);
    }
}
