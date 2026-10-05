using Engineering.Domain.Entities.ContractorContracts;
using Engineering.Domain.Entities.ContractorStatusStatements;

namespace Engineering.Persistence.Configurations.ContractorContracts;

public class ContractorContractHeaderVersionConfiguration : IEntityTypeConfiguration<ContractorContractHeaderVersion>
{
    private const string TableName = "ContractorContractHeaderVersions";
    public void Configure(EntityTypeBuilder<ContractorContractHeaderVersion> builder)
    {
        builder.MetaConfiguration<ContractorContractHeaderVersion, long>(TableName);

        builder.Property(oo => oo.Content)
            .HasComment(CContractCmts.Content)
            .HasColumnType("nvarchar(MAX)")
            .IsRequired();

        builder.Property(oo => oo.Version)
            .HasComment(CContractCmts.Version)
            .HasColumnType("int")
            .IsRequired();

        builder.Property(oo => oo.CreatedPaymentDate)
            .HasComment(CContractCmts.CreatedPaymentDate)
            .HasColumnType("datetime")
            .IsRequired(false);

        builder.Property(oo => oo.ContractorContractHeaderId)
            .HasComment(CContractCmts.ContractorContractHeaderId)
            .HasColumnType("bigint")
            .IsRequired();

        builder.HasOne(oo => oo.ContractorContractHeader)
           .WithMany(oo => oo.ContractorContractHeaderVersions)
           .HasForeignKey("ContractorContractHeaderId")
           .HasPrincipalKey(nameof(ContractorContractHeader.Id))
           .OnDelete(DeleteBehavior.Restrict);

        builder.Property(oo => oo.ContractorStatusStatementId)
            .HasComment(CContractCmts.ContractorStatusStatementId)
            .HasColumnType("bigint")
            .IsRequired(false);

        builder.HasOne(oo => oo.ContractorStatusStatement)
           .WithMany(oo => oo.ContractorContractHeaderVersions)
           .HasForeignKey("ContractorStatusStatementId")
           .HasPrincipalKey(nameof(ContractorStatusStatement.Id))
           .OnDelete(DeleteBehavior.Restrict);
    }
}
