using Engineering.Domain.Entities.EmployerContracts;

namespace Engineering.Persistence.Configurations.ContractorContracts;

public class EmployerContractHistoryConfiguration : IEntityTypeConfiguration<EmployerContractHistory>
{
    private const string TableName = "EmployerContractHistories";
    public void Configure(EntityTypeBuilder<EmployerContractHistory> builder)
    {
        builder.MetaActiveConfiguration<EmployerContractHistory, long>(TableName);

        builder.Property(oo => oo.IsFirst)
            .HasComment(EContractCmts.IsFirst)
            .IsRequired();

        builder.Property(oo => oo.Code)
            .HasComment(GlobalCmts.Code)
            .HasColumnType("nvarchar(250)")
            .HasMaxLength(250);

        builder.Property(oo => oo.Status)
            .HasComment(EContractCmts.ContractStatus)
            .IsRequired();

        builder.Property(oo => oo.StartDate)
            .HasComment(GlobalCmts.StartDate);

        builder.Property(oo => oo.EndDate)
            .HasComment(GlobalCmts.EndDate);

        builder.Property(oo => oo.TotalAmount)
            .HasComment(EContractCmts.TotalAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.Description)
            .HasComment(GlobalCmts.Description)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.AdvancePayment)
            .HasComment(EContractCmts.AdvancePayment)
            .HasColumnType("decimal(18,2)");

        builder.HasOne(oo => oo.EmployerContract)
            .WithMany(oo => oo.EmployerContractHistory)
            .HasForeignKey("EmployerContractId")
            .HasPrincipalKey(nameof(EmployerContract.Id))
            .OnDelete(DeleteBehavior.Restrict);
    }
}
