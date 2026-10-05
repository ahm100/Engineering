using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Histories;

namespace Engineering.Persistence.Configurations.RequestGoodsSupplies.Histories;

internal class RequestGoodsSupplyManagementHistoryConfiguration : IEntityTypeConfiguration<RequestGoodsSupplyManagementHistory>
{
    private const string _tableName = "RequestGoodsSupplyDetailManagementHistories";
    public void Configure(EntityTypeBuilder<RequestGoodsSupplyManagementHistory> builder)
    {
        builder.ToTable(_tableName);
        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(oo => oo.Created)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(oo => oo.CreatorId)
            .IsRequired();

        builder.Property(oo => oo.RequestedCount)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oo => oo.Type)
            .IsRequired();

        builder.Property(oo => oo.Description)
            .HasColumnType("nvarchar(1500)")
            .HasDefaultValue(null);


        builder.HasOne(oo => oo.RequestGoodsSupplyManagement)
            .WithMany(oo => oo.Histories)
            .HasForeignKey("RequestGoodsSupplyManagementId")
            .HasPrincipalKey(nameof(RequestGoodsSupplyManagement.Id))
            .OnDelete(DeleteBehavior.Restrict);
    }
}
