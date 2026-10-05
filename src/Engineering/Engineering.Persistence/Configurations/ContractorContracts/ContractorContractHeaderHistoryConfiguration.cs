using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Persistence.Configurations.ContractorContracts;

public class ContractorContractHeaderHistoryConfiguration : IEntityTypeConfiguration<ContractorContractHeaderHistory>
{
    private const string TableName = "ContractorContractHeaderHistories";
    public void Configure(EntityTypeBuilder<ContractorContractHeaderHistory> builder)
    {
        builder.MetaConfiguration<ContractorContractHeaderHistory, long>(TableName);

        builder.Property(oo => oo.ContractorId)
            .IsRequired();

        builder.Property(oo => oo.CurrencyId)
            .IsRequired();

        builder.Property(oo => oo.Status)
            .IsRequired();

        builder.Property(oo => oo.StartDate);

        builder.Property(oo => oo.EndDate);

        builder.Property(oo => oo.FinalTotalAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.TotalPercentageDoingJobWell)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.TotalDoingJobWellAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.TotalPercentageAdvancePayment)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.TotalAdvancePaymentAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.TotalDailyLatenessPenalty)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.TotalWorkDonePercent)
            .HasColumnType("decimal(5,2)");

        builder.Property(oo => oo.TotalWorkDeliveryPercent)
            .HasColumnType("decimal(5,2)");

        builder.Property(oo => oo.TotalWorkCompletionPercent)
            .HasColumnType("decimal(5,2)");

        builder.Property(oo => oo.Description)
            .HasColumnType("nvarchar(1500)");

        builder.HasOne(oo => oo.ContractorContractHeader)
            .WithMany(oo => oo.ContractorContractHeaderHistories)
            .HasForeignKey("ContractorContractHeaderId")
            .HasPrincipalKey(nameof(ContractorContractHeader.Id))
            .OnDelete(DeleteBehavior.Restrict);
    }
}
