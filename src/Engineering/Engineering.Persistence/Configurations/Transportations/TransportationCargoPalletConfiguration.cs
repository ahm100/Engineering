using Engineering.Domain.Entities.Transportations;

namespace Engineering.Persistence.Configurations.Transportations;

public class TransportationCargoPalletConfiguration : IEntityTypeConfiguration<TransportationCargoPallet>
{
    private const string _tableName = "TransportationCargoPallets";
    public void Configure(EntityTypeBuilder<TransportationCargoPallet> builder)
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

        builder.Property(a => a.TransferPrice)
            .HasColumnType("decimal(18, 2)")
            .IsRequired(false);

        builder.Property(a => a.PalletNumber)
            .IsRequired(false);

        builder.Property(a => a.Weight)
            .HasColumnType("decimal(18, 2)")
            .IsRequired(false);

        builder.Property(a => a.Quantity)
            .HasColumnType("decimal(18, 2)")
            .IsRequired(false);

        builder.Property(a => a.RefrenceId)
            .IsRequired(false);

        builder.Property(a => a.DeliveryMethod)
            .IsRequired(false);

        builder.Property(a => a.DeliveryType)
            .IsRequired(false);

        builder.Property(a => a.VehicleName)
            .HasColumnType("nvarchar(250)")
            .IsRequired(false);

        builder.Property(a => a.NumberPlate)
            .HasColumnType("nvarchar(250)")
            .IsRequired(false);

        builder.Property(a => a.Driver)
            .HasColumnType("nvarchar(250)")
            .IsRequired(false);

        builder.Property(a => a.DriverPhoneNumber)
            .HasColumnType("nvarchar(250)")
            .IsRequired(false);

        builder.Property(a => a.PackingShippingType)
            .IsRequired(false);

        builder.Property(a => a.PostageDate)
            .IsRequired(false);

        builder.HasOne(oo => oo.TransportationRequest)
            .WithMany(oo => oo.TransportationCargoPallets)
            .HasForeignKey(x => x.TransportationRequestId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

        builder.HasOne(oo => oo.TransportationContractor)
            .WithMany(oo => oo.TransportationCargoPallets)
            .HasForeignKey(x => x.TransportationContractorId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

        builder.HasOne(oo => oo.TransportationCargo)
            .WithMany(oo => oo.TransportationCargoPallets)
            .HasForeignKey(x => x.TransportationCargoId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(oo => oo.ShippingCost)
            .WithMany(oo => oo.TransportationCargoPallets)
            .HasForeignKey(x => x.ShippingCostId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(oo => oo.PackingPallet)
            .WithMany()
            .HasForeignKey(x => x.PackingPalletId)
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
