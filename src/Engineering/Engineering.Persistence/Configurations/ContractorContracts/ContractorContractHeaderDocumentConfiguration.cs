using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Persistence.Configurations.ContractorContracts;

public class ContractorContractHeaderDocumentConfiguration : IEntityTypeConfiguration<ContractorContractHeaderDocument>
{
    private const string TableName = "ContractorContractHeaderDocuments";
    public void Configure(EntityTypeBuilder<ContractorContractHeaderDocument> builder)
    {
        builder.MetaConfiguration<ContractorContractHeaderDocument, long>(TableName);

        builder.Property(oo => oo.Url)
            .HasColumnType("nvarchar(1500)")
            .IsRequired();

        builder.HasOne(oo => oo.ContractorContractHeader)
               .WithMany(oo => oo.ContractorContractHeaderDocuments)
               .HasForeignKey("ContractorContractHeaderId")
               .HasPrincipalKey(nameof(ContractorContractHeader.Id))
               .OnDelete(DeleteBehavior.Cascade);
    }
}
