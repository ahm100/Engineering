using Engineering.Domain.Entities.ContractorStatusStatements;

namespace Engineering.Persistence.Configurations.ContractorStatusStatements;

public class ContractorStatusStatementPaymentConfiguration : IEntityTypeConfiguration<ContractorStatusStatementPayment>
{
    private const string TableName = "ContractorStatusStatementPayments";
    public void Configure(EntityTypeBuilder<ContractorStatusStatementPayment> builder)
    {
        builder.MetaConfiguration<ContractorStatusStatementPayment, long>(TableName);

        builder.Property(oo => oo.CalculatedAmount)
            .HasColumnType("decimal(18,2)")
            .HasComment(CSSCmts.CalculatedAmount)
            .IsRequired();

        builder.Property(oo => oo.PayableAmount)
            .HasColumnType("decimal(18,2)")
            .HasComment(CSSCmts.PayableAmount)
            .IsRequired();

        builder.Property(oo => oo.UserAmount)
            .HasColumnType("decimal(18,2)")
            .HasComment(CSSCmts.UserAmount)
            .IsRequired();

        builder.Property(oo => oo.UserDescription)
            .HasComment(CSSCmts.UserDescription)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.Status)
            .IsRequired();

        builder.Property(oo => oo.PaymentOrderId)
            .HasComment(CSSCmts.PaymentOrderId)
            .HasColumnType("bigint");

        builder.Property(oo => oo.ProjectManagerAmount)
            .HasComment(CSSCmts.ProjectManagerAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.ProjectManagerDescription)
            .HasComment(CSSCmts.ProjectManagerDescription)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.ManagementAmount)
            .HasComment(CSSCmts.ManagementAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.ManagementDescription)
            .HasComment(CSSCmts.ManagementDescription)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.PrimaryManagerAmount)
            .HasComment(CSSCmts.PrimaryManagerAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.PrimaryManagerDescription)
            .HasComment(CSSCmts.PrimaryManagerDescription)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.FinalManagerAmount)
            .HasComment(CSSCmts.FinalManagerAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.FinalManagerDescription)
            .HasComment(CSSCmts.FinalManagerDescription)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.PaymentAmount)
            .HasComment(CSSCmts.PaymentAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.PaymentDescription)
            .HasComment(CSSCmts.PaymentDescription)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.PaymentOrderId)
            .HasComment(CSSCmts.PaymentOrderId);

        builder.Property(oo => oo.PaymentDate)
            .HasComment(CSSCmts.PaymentDate)
            .HasColumnType("datetime");

        builder.Property(oo => oo.TreasuryPaid)
            .HasColumnType("decimal(18,2)")
            .HasComment(CSSCmts.TreasuryPaid);

        builder
            .HasOne(oo => oo.ContractorStatusStatement)
            .WithMany(oo => oo.ContractorStatusStatementPayments)
            .HasForeignKey("ContractorStatusStatementId")
            .HasPrincipalKey(nameof(ContractorStatusStatement.Id));
    }
}
