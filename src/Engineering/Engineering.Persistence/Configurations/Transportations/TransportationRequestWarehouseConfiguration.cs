using Engineering.Domain.Entities.Transportations;

namespace Engineering.Persistence.Configurations.Transportations;

public class TransportationRequestWarehouseConfiguration : IEntityTypeConfiguration<TransportationRequestWarehouse>
{
    private const string _tableName = "TransportationRequestWarehouses";
    public void Configure(EntityTypeBuilder<TransportationRequestWarehouse> builder)
    {
        builder.ToTable(_tableName);

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(a => a.IsDeleted).IsRequired();

        builder.Property(a => a.Updated);

        builder.Property(a => a.Created).IsRequired();

        builder.Property(a => a.UpdaterId);

        builder.Property(a => a.Price)
            .HasColumnType("decimal(18, 2)")
            .IsRequired(false);

        builder.Property(a => a.PalletNumber)
            .IsRequired(false);

        builder.Property(a => a.Quantity)
            .HasColumnType("decimal(18, 2)")
            .IsRequired(false);

        builder.HasOne(oo => oo.TransportationCargoPallet)
            .WithMany(oo => oo.TransportationRequestWarehouses)
            .HasForeignKey(x => x.TransportationCargoPalletId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(oo => oo.PackingProduct)
            .WithMany()
            .HasForeignKey(x => x.PackingProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(oo => oo.PackingDestinationAddress)
            .WithMany()
            .HasForeignKey(x => x.PackingDestinationAddressId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(oo => oo.PackingSourceAddress)
            .WithMany()
            .HasForeignKey(x => x.PackingSourceAddressId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
