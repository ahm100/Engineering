using Engineering.Domain.Entities.Contracts;

namespace Engineering.Persistence.Configurations.Contracts;

public class ContractStatusHistoryDocumentConfiguration
    : IEntityTypeConfiguration<ContractStatusHistoryDocument>
{
    private const string TableName = "ContractStatusHistoryDocuments";

    public void Configure(EntityTypeBuilder<ContractStatusHistoryDocument> builder)
    {
        builder.MetaConfiguration<ContractStatusHistoryDocument, long>(TableName);

        builder.Property(oo => oo.ContractStatusHistoryId).IsRequired();
        builder.Property(oo => oo.Url)
            .HasMaxLength(1500)
            .HasComment(GlobalCmts.Url)
            .IsRequired();

        builder.HasOne(oo => oo.ContractStatusHistory)
            .WithMany(oo => oo.Documents)
            .HasForeignKey(oo => oo.ContractStatusHistoryId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}
