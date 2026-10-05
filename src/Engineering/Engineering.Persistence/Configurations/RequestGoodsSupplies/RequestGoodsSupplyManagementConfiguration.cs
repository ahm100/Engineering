using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Persistence.Configurations.RequestGoodsSupplies;

public class RequestGoodsSupplyManagementConfiguration : IEntityTypeConfiguration<RequestGoodsSupplyManagement>
{
    private const string _tableName = "RequestGoodsSupplyDetailManagements";
    public void Configure(EntityTypeBuilder<RequestGoodsSupplyManagement> builder)
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

        builder.Property(oo => oo.ReferenceId);

        builder.Property(oo => oo.DestinationWarehouseId);

        builder.Property(oo => oo.OperatorAppointmentId);

        builder.Property(oo => oo.CreatorId)
            .IsRequired();

        builder.Property(oo => oo.RequestedCount)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oo => oo.ConfirmedRequestCount)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.Type)
            .IsRequired();

        builder.Property(oo => oo.Description)
            .HasColumnType("nvarchar(1500)")
            .HasDefaultValue(null);

        builder.Property(oo => oo.LastDescription)
            .HasColumnType("nvarchar(1500)");

        builder.HasOne(oo => oo.RequestGoodsSupplyDetail)
            .WithMany(oo => oo.RequestGoodsSupplyManagements)
            .HasForeignKey("RequestGoodsSupplyDetailId")
            .HasPrincipalKey(nameof(RequestGoodsSupplyDetail.Id))
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(oo => oo.RequestGoodsSupplyProduct)
               .WithMany(oo => oo.RequestGoodsSupplyManagements)
               .HasForeignKey("RequestGoodsSupplyProductId")
               .HasPrincipalKey(nameof(RequestGoodsSupplyProduct.Id))
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(oo => oo.RequestGoodsSupplyType)
               .WithMany(oo => oo.RequestGoodsSupplyManagements)
               .HasForeignKey("RequestGoodsSupplyTypeId")
               .HasPrincipalKey(nameof(RequestGoodsSupplyType.Id))
               .OnDelete(DeleteBehavior.Restrict);
    }
}
