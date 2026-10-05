using Engineering.Domain.Entities.Logistics;

namespace Engineering.Persistence.Configurations.Logistics;

public class TransportationContractorDocumentConfiguration : IEntityTypeConfiguration<TransportationContractorDocument>
{
    private const string _tableName = "TransportationContractorDocuments";
    public void Configure(EntityTypeBuilder<TransportationContractorDocument> builder)
    {
        builder.MetaActiveConfiguration<TransportationContractorDocument, long>(_tableName);

        builder.Property(a => a.DocumentUrl)
            .HasComment(GlobalCmts.Url)
            .IsRequired();

        builder.HasOne(x => x.TransportationContractor)
            .WithMany(x => x.ContractorDocuments)
            .HasForeignKey(x => x.TransportationContractorId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
