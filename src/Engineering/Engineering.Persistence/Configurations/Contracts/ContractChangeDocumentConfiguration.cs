using Engineering.Domain.Entities.Contracts;

namespace Engineering.Persistence.Configurations.Contracts;

public class ContractChangeDocumentConfiguration
    : IEntityTypeConfiguration<ContractChangeDocument>
{
    private const string TableName = "ContractChangeDocuments";

    public void Configure(EntityTypeBuilder<ContractChangeDocument> builder)
    {
        builder.MetaConfiguration<ContractChangeDocument, long>(TableName);

        builder.Property(oo => oo.ContractChangeId).IsRequired();
        builder.Property(oo => oo.Url)
            .HasMaxLength(1500)
            .HasComment(GlobalCmts.Url)
            .IsRequired();

        builder.HasOne(oo => oo.ContractChange)
            .WithMany(oo => oo.Documents)
            .HasForeignKey(oo => oo.ContractChangeId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}
