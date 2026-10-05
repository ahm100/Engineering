using Engineering.Domain.Entities.Contracts;

namespace Engineering.Persistence.Configurations.Contracts;

public class ContractChangeConfiguration : IEntityTypeConfiguration<ContractChange>
{
    private const string TableName = "ContractChanges";

    public void Configure(EntityTypeBuilder<ContractChange> builder)
    {
        builder.MetaConfiguration<ContractChange, long>(TableName);

        builder.Property(oo => oo.ContractId).IsRequired();
        builder.Property(oo => oo.Mode)
            .HasComment(ContractCmts.ContractChangeMode)
            .IsRequired();
        builder.Property(oo => oo.Type)
            .HasComment(ContractCmts.ContractChangeType)
            .IsRequired();
        builder.Property(oo => oo.Number)
            .HasMaxLength(250)
            .HasComment(ContractCmts.ContractChangeNumber)
            .IsRequired();
        builder.Property(oo => oo.Date)
            .HasComment(GlobalCmts.Date)
            .IsRequired();
        builder.Property(oo => oo.Subject)
            .HasMaxLength(250)
            .HasComment(ContractCmts.ContractChangeSubject)
            .IsRequired();
        builder.Property(oo => oo.DurationChange)
            .HasComment(ContractCmts.ContractChangeDuration);
        builder.Property(oo => oo.PreviousContractAmount)
            .HasColumnType("decimal(18,2)")
            .HasComment(ContractCmts.PreviousContractAmount)
            .IsRequired();
        builder.Property(oo => oo.FinancialChangeAmount)
            .HasColumnType("decimal(18,2)")
            .HasComment(ContractCmts.FinancialChangeAmount)
            .IsRequired();
        builder.Property(oo => oo.FinalContractAmount)
            .HasColumnType("decimal(18,2)")
            .HasComment(ContractCmts.FinalContractAmount)
            .IsRequired();

        builder.HasIndex(oo => new { oo.ContractId, oo.Number })
            .IsUnique()
            .HasFilter("[IsDeleted] = 0");

        builder.HasOne(oo => oo.Contract)
            .WithMany(oo => oo.ContractChanges)
            .HasForeignKey(oo => oo.ContractId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}
