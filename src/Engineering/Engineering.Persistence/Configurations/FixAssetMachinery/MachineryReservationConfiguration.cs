using Engineering.Domain.Entities.FixAssetMachineries;
using Engineering.Domain.Entities.FixAssetMachineries.Enums;
using Engineering.Domain.Entities.RequestMachineries;
using FixAssetMachineryModel = Engineering.Domain.Entities.FixAssetMachineries.FixAssetMachinery;

namespace Engineering.Persistence.Configurations.FixAssetMachineries;

public class MachineryReservationConfiguration : IEntityTypeConfiguration<MachineryReservation>
{
    private const string _tableName = "MachineryReservations";
    public void Configure(EntityTypeBuilder<MachineryReservation> builder)
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
        .ValueGeneratedOnAdd().
        IsRequired();

        builder.Property(oo => oo.CreatorId)
            .IsRequired();

        builder.Property(oo => oo.StartDate)
            .IsRequired();

        builder.Property(oo => oo.EndDate)
            .IsRequired();

        builder.Property(oo => oo.Status)
            .HasDefaultValue(MachineryReservationStatus.NotsStarted)
            .IsRequired();

        builder.Property(oo => oo.Unit)
            .IsRequired();

        builder.Property(oo => oo.Description)
            .HasColumnType("nvarchar(1500)");

        builder.HasOne(oo => oo.FixAssetMachinery)
               .WithMany(oo => oo.MachineryReservations)
               .HasForeignKey("FixAssetMachineryId")
               .HasPrincipalKey(nameof(FixAssetMachineryModel.Id))
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(oo => oo.RequestMachinery)
               .WithMany(oo => oo.MachineryReservations)
               .HasForeignKey("RequestMachineryId")
               .HasPrincipalKey(nameof(RequestMachinery.Id))
               .OnDelete(DeleteBehavior.Restrict);
    }
}
