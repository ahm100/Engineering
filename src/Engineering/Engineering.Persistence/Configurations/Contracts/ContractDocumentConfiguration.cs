using Engineering.Domain.Entities.Contracts;

namespace Engineering.Persistence.Configurations.Contracts;

public class ContractDocumentConfiguration : IEntityTypeConfiguration<ContractDocument>
{
    private const string TableName = "ContractDocuments";

    public void Configure(EntityTypeBuilder<ContractDocument> builder)
    {
        builder.MetaConfiguration<ContractDocument, long>(TableName);

        builder.Property(oo => oo.Url)
            .HasComment(GlobalCmts.Url)
            .HasMaxLength(1500)
            .IsRequired();

        builder.Property(oo => oo.ContractId)
            .HasComment(GlobalCmts.ContractId)
            .IsRequired();

        builder.HasOne(oo => oo.Contract)
            .WithMany(oo => oo.ContractDocuments)
            .HasForeignKey(oo => oo.ContractId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}