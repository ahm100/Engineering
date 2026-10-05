using Engineering.Domain.Entities.Contracts;

namespace Engineering.Persistence.Configurations.Contracts;

public class ContractGuaranteeConfiguration : IEntityTypeConfiguration<ContractGuarantee>
{
    private const string TableName = "ContractGuarantees";

    public void Configure(EntityTypeBuilder<ContractGuarantee> builder)
    {
        builder.MetaConfiguration<ContractGuarantee, long>(TableName);

        builder.Property(oo => oo.ContractId)
            .HasComment(GlobalCmts.ContractId)
            .IsRequired();

        builder.Property(oo => oo.Type)
            .HasComment(ContractCmts.ContractGuaranteeType)
            .IsRequired();

        builder.Property(oo => oo.Amount)
            .HasColumnType("decimal(18,2)")
            .HasComment(ContractCmts.ContractGuaranteeAmount)
            .IsRequired();

        builder.Property(oo => oo.Percentage)
            .HasColumnType("decimal(5,2)")
            .HasComment(ContractCmts.ContractGuaranteePercentage);

        builder.Property(oo => oo.Number)
            .HasMaxLength(250)
            .HasComment(ContractCmts.ContractGuaranteeNumber)
            .IsRequired();

        builder.Property(oo => oo.IssueDate)
            .HasComment(ContractCmts.ContractGuaranteeIssueDate)
            .IsRequired();

        builder.Property(oo => oo.ExpiryDate)
            .HasComment(ContractCmts.ContractGuaranteeExpiryDate)
            .IsRequired();

        builder.Property(oo => oo.Status)
            .HasComment(ContractCmts.ContractGuaranteeStatus)
            .IsRequired();

        builder.Property(oo => oo.FileUrl)
            .HasMaxLength(1500)
            .HasComment(ContractCmts.ContractGuaranteeFileUrl);

        builder.HasIndex(oo => oo.ContractId);

        builder.HasOne(oo => oo.Contract)
            .WithMany(oo => oo.Guarantees)
            .HasForeignKey(oo => oo.ContractId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}
