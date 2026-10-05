using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Documents;

namespace Engineering.Persistence.Configurations.RequestGoodsSupplies.Documents;

public class RequestGoodsSupplyTypeDetailDocumentConfiguration : IEntityTypeConfiguration<RequestGoodsSupplyTypeDetailDocument>
{
    private const string TableName = "RequestGoodsSupplyTypeDetailDocuments";
    public void Configure(EntityTypeBuilder<RequestGoodsSupplyTypeDetailDocument> builder)
    {
        builder.MetaConfiguration<RequestGoodsSupplyTypeDetailDocument, long>(TableName);

        builder.Property(oo => oo.Url)
        .HasColumnType("nvarchar(1500)")
        .IsRequired();

        builder.HasOne(oo => oo.RequestGoodsSupplyTypeDetail)
               .WithMany(oo => oo.RequestGoodsSupplyTypeDetailDocuments)
               .HasForeignKey("RequestGoodsSupplyTypeDetailId")
               .HasPrincipalKey(nameof(RequestGoodsSupplyTypeDetail.Id))
               .OnDelete(DeleteBehavior.NoAction);
    }
}
