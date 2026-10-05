using Engineering.Domain.Entities.FixAssetMachineries;
using FixAssetMachineryModel = Engineering.Domain.Entities.FixAssetMachineries.FixAssetMachinery;

namespace Engineering.Persistence.Configurations.FixAssetMachineries;

public class FixAssetMachineryRateConfiguration : IEntityTypeConfiguration<FixAssetMachineryRate>
{
    private const string _tableName = "FixAssetMachineryRates";
    public void Configure(EntityTypeBuilder<FixAssetMachineryRate> builder)
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

        builder.Property(oo => oo.HourlyRate);

        builder.Property(oo => oo.DailyRate);

        builder.Property(oo => oo.ServiceRate);

        builder.Property(oo => oo.VolumeRate);

        builder.HasOne(oo => oo.FixAssetMachinery)
               .WithMany(oo => oo.FixAssetMachineryRates)
               .HasForeignKey("FixAssetMachineryId")
               .HasPrincipalKey(nameof(FixAssetMachineryModel.Id))
               .OnDelete(DeleteBehavior.Restrict);
    }
}
