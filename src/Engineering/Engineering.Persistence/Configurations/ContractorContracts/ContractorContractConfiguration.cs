using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Persistence.Configurations.ContractorContracts;

public class ContractorContractConfiguration : IEntityTypeConfiguration<ContractorContract>
{
    private const string TableName = "ContractorContracts";
    public void Configure(EntityTypeBuilder<ContractorContract> builder)
    {
        builder.MetaConfiguration<ContractorContract, long>(TableName);

        builder.Property(oo => oo.CompanyId)
            .HasComment(GlobalCmts.CompanyId)
            .IsRequired();

        builder.Property(oo => oo.StartDate)
            .IsRequired();

        builder.Property(oo => oo.EndDate)
            .IsRequired();

        builder.Property(oo => oo.TotalAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.PercentageDoingJobWell)
            .HasColumnType("decimal(5,2)");

        builder.Property(oo => oo.TotalCostOveredAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.DoingJobWellAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.PercentageAdvancePayment)
            .HasColumnType("decimal(5,2)");

        builder.Property(oo => oo.AdvancePaymentAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.DailyLatenessPenalty)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.DailyBaseHours)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.MonthlyBaseHours)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.Description)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.ContractorContractType)
            .IsRequired();

        builder.HasOne(oo => oo.ContractorContractHeader)
            .WithMany(oo => oo.ContractorContracts)
            .HasForeignKey("ContractorContractHeaderId").HasPrincipalKey(nameof(ContractorContractHeader.Id))
            .OnDelete(DeleteBehavior.NoAction);

        builder.Property(oo => oo.ProjectId)
            .IsRequired(false);
        builder.HasOne(oo => oo.Project)
               .WithMany(oo => oo.ContractorContracts)
               .HasForeignKey(oo => oo.ProjectId)
               .OnDelete(DeleteBehavior.Restrict)
               .IsRequired();
    }
}
