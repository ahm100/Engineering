using Engineering.Domain.Entities.Contracts;

namespace Engineering.Persistence.Configurations.Contracts;

public class ContractFinancialInformationConfiguration
    : IEntityTypeConfiguration<ContractFinancialInformation>
{
    private const string TableName = "ContractFinancialInformations";

    public void Configure(
        EntityTypeBuilder<ContractFinancialInformation> builder)
    {
        builder.MetaConfiguration<ContractFinancialInformation, long>(
            TableName);

        builder.Property(oo => oo.ContractId)
            .HasComment(GlobalCmts.ContractId)
            .IsRequired();

        builder.Property(oo => oo.CurrencyId)
            .HasComment(GlobalCmts.CurrencyId)
            .IsRequired();

        builder.Property(oo => oo.HasPrepayment)
            .HasComment(ContractCmts.HasPrepayment)
            .IsRequired();

        builder.Property(oo => oo.IsSubjectToAdjustment)
            .HasComment(ContractCmts.IsSubjectToAdjustment)
            .IsRequired();

        builder.Property(oo => oo.RegisteredInitialAmount)
            .HasColumnType("decimal(18,2)")
            .HasComment(ContractCmts.RegisteredInitialAmount)
            .IsRequired(false);

        builder.Property(oo => oo.ContractCeilingAmount)
            .HasColumnType("decimal(18,2)")
            .HasComment(ContractCmts.ContractCeilingAmount);

        builder.Property(oo => oo.AdjustmentLimitValue)
            .HasColumnType("decimal(18,2)")
            .HasComment(ContractCmts.AdjustmentLimitValue);

        builder.Property(oo => oo.AdjustmentLimitType)
            .HasComment(ContractCmts.AdjustmentLimitType);

        builder.Property(oo => oo.PrepaymentPercentage)
            .HasColumnType("decimal(5,2)")
            .HasComment(ContractCmts.PrepaymentPercentage);

        builder.Property(oo => oo.PrepaymentAmortizationMethod)
            .HasComment(ContractCmts.PrepaymentAmortizationMethod);

        builder.Property(oo => oo.PrepaymentAmortizationValue)
            .HasColumnType("decimal(18,2)")
            .HasComment(ContractCmts.PrepaymentAmortizationValue);

        builder.Property(oo => oo.PrepaymentStartStatusStatementNumber)
            .HasComment(ContractCmts.PrepaymentStartStatusStatementNumber);

        builder.Property(oo => oo.PrepaymentStartProgressPercentage)
            .HasColumnType("decimal(5,2)")
            .HasComment(ContractCmts.PrepaymentStartProgressPercentage);

        builder.HasIndex(oo => oo.ContractId)
            .IsUnique()
            .HasFilter("[IsDeleted] = 0");

        builder.HasOne(oo => oo.Contract)
            .WithOne(oo => oo.FinancialInformation)
            .HasForeignKey<ContractFinancialInformation>(
                oo => oo.ContractId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}
