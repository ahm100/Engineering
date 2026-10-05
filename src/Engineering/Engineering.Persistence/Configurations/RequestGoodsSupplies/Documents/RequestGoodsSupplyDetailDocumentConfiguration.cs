using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Documents;

namespace Engineering.Persistence.Configurations.RequestGoodsSupplies.Documents;

public class RequestGoodsSupplyDetailDocumentConfiguration : IEntityTypeConfiguration<RequestGoodsSupplyDetailDocument>
{
    private const string _tableName = "RequestGoodsSupplyDetailDocuments";
    public void Configure(EntityTypeBuilder<RequestGoodsSupplyDetailDocument> builder)
    {
        builder.ToTable(_tableName);
        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(oo => oo.Url)
            .HasColumnType("nvarchar(1500)")
            .IsRequired();

        builder.Property(oo => oo.Created)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(oo => oo.CreatorId)
            .IsRequired();


        builder.HasOne(oo => oo.RequestGoodsSupplyDetail)
               .WithMany(oo => oo.RequestGoodsSupplyDetailDocuments)
               .HasForeignKey("RequestGoodsSupplyDetailId")
               .HasPrincipalKey(nameof(RequestGoodsSupplyDetail.Id))
               .OnDelete(DeleteBehavior.Cascade);
    }
}
