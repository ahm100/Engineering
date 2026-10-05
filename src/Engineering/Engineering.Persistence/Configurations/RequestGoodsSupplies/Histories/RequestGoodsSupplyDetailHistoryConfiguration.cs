using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Histories;

namespace Engineering.Persistence.Configurations.RequestGoodsSupplies.Histories;

public class RequestGoodsSupplyDetailHistoryConfiguration : IEntityTypeConfiguration<RequestGoodsSupplyDetailHistory>
{
    private const string _tableName = "RequestGoodsSupplyDetailHistories";
    public void Configure(EntityTypeBuilder<RequestGoodsSupplyDetailHistory> builder)
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

        builder.Property(oo => oo.Description)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.ManagementDescription)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.CreatorId)
            .IsRequired();

        builder.Property(oo => oo.Status)
            .IsRequired();

        builder.Property(oo => oo.RequestedCount)
            .HasColumnType("decimal(18,2)")
             .IsRequired();

        builder.Property(oo => oo.UnitPrice)
             .HasColumnType("decimal(18, 2)")
             .HasDefaultValue(null);

        builder.Property(oo => oo.TotalPrice)
             .HasColumnType("decimal(18, 2)")
             .HasDefaultValue(null);

        builder.Property(oo => oo.DiscountByNumber)
             .HasColumnType("decimal(18, 2)")
             .HasDefaultValue(null);

        builder.Property(oo => oo.DiscountByPercentage)
             .HasColumnType("decimal(5, 2)")
             .HasDefaultValue(null);

        builder.Property(oo => oo.DiscountedPrice)
             .HasColumnType("decimal(18, 2)")
             .HasDefaultValue(null);

        builder.Property(oo => oo.TaxNumber)
             .HasColumnType("decimal(18, 2)")
             .HasDefaultValue(null);

        builder.Property(oo => oo.PackingPrice)
             .HasColumnType("decimal(18, 2)")
             .HasDefaultValue(null);

        builder.Property(oo => oo.TaxPercentage)
             .HasColumnType("decimal(5, 2)")
             .HasDefaultValue(null);

        builder.Property(oo => oo.FinalPrice)
             .HasColumnType("decimal(18, 2)")
             .HasDefaultValue(null);

        builder.Property(oo => oo.CustomerInvoiceNumber)
            .HasColumnType("nvarchar(50)");


        builder.HasOne(oo => oo.RequestGoodsSupplyDetail)
               .WithMany(oo => oo.RequestGoodsSupplyDetailHistories)
               .HasForeignKey("RequestGoodsSupplyDetailId")
               .HasPrincipalKey(nameof(RequestGoodsSupplyDetail.Id))
               .OnDelete(DeleteBehavior.NoAction);
    }
}
