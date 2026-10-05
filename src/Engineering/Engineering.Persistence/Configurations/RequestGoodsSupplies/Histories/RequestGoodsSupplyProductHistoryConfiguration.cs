using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Histories;

namespace Engineering.Persistence.Configurations.RequestGoodsSupplies.Histories;

public class RequestGoodsSupplyProductHistoryConfiguration : IEntityTypeConfiguration<RequestGoodsSupplyProductHistory>
{
    private const string _tableName = "RequestGoodsSupplyProductHistories";
    public void Configure(EntityTypeBuilder<RequestGoodsSupplyProductHistory> builder)
    {
        builder.ToTable(_tableName);
        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(oo => oo.Status)
            .IsRequired();

        builder.Property(oo => oo.Created)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(oo => oo.CreatorId)
            .IsRequired();

        builder.HasOne(oo => oo.RequestGoodsSupplyProduct)
               .WithMany(oo => oo.RequestGoodsSupplyProductHistories)
               .HasForeignKey("RequestGoodsSupplyProductId")
               .HasPrincipalKey(nameof(RequestGoodsSupplyProduct.Id))
               .OnDelete(DeleteBehavior.NoAction);
    }
}
