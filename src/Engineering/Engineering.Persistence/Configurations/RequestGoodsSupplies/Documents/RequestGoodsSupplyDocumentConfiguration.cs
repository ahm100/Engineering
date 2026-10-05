using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Documents;

namespace Engineering.Persistence.Configurations.RequestGoodsSupplies.Documents;

public class RequestGoodsSupplyDocumentConfiguration : IEntityTypeConfiguration<RequestGoodsSupplyDocument>
{
    private const string TableName = "RequestGoodsSupplyDocuments";
    public void Configure(EntityTypeBuilder<RequestGoodsSupplyDocument> builder)
    {
        builder.MetaConfiguration<RequestGoodsSupplyDocument, long>(TableName);

        builder.Property(oo => oo.Url)
        .HasColumnType("nvarchar(1500)")
        .IsRequired();

        builder.HasOne(oo => oo.RequestGoodsSupply)
               .WithMany(oo => oo.RequestGoodsSupplyDocuments)
               .HasForeignKey("RequestGoodsSupplyId")
               .HasPrincipalKey(nameof(RequestGoodsSupply.Id))
               .OnDelete(DeleteBehavior.NoAction);
    }
}
