using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Histories;

namespace Engineering.Persistence.Configurations.RequestGoodsSupplies.Histories;

public class RequestGoodsSupplyHistoryConfiguration : IEntityTypeConfiguration<RequestGoodsSupplyHistory>
{
    private const string _tableName = "RequestGoodsSupplyHistories";
    public void Configure(EntityTypeBuilder<RequestGoodsSupplyHistory> builder)
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


        builder.HasOne(oo => oo.RequestGoodsSupply)
               .WithMany(oo => oo.RequestGoodsSupplyHistories)
               .HasForeignKey("RequestGoodsSupplyId")
               .HasPrincipalKey(nameof(RequestGoodsSupply.Id))
               .OnDelete(DeleteBehavior.NoAction);
    }
}
