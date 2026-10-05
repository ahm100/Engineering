using Engineering.Domain.Entities.FixAssetMachineries.Enums;
using Engineering.Domain.Entities.Machineries;
using FixAssetMachineryModel = Engineering.Domain.Entities.FixAssetMachineries.FixAssetMachinery;

namespace Engineering.Persistence.Configurations.FixAssetMachineries;

public class FixAssetMachineryConfiguration : IEntityTypeConfiguration<FixAssetMachineryModel>
{
    private const string _tableName = "FixAssetMachineries";
    public void Configure(EntityTypeBuilder<FixAssetMachineryModel> builder)
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

        builder.Property(oo => oo.CompanyId);

        builder.Property(oo => oo.CreatorId)
            .IsRequired();

        builder.Property(oo => oo.StartDate);

        builder.Property(oo => oo.EndDate);

        builder.Property(oo => oo.DriverId);

        builder.Property(oo => oo.DriverName)
          .HasColumnType("nvarchar(250)");

        builder.Property(oo => oo.MachinerySpecification)
          .HasColumnType("nvarchar(1000)");

        builder.Property(oo => oo.NumberPlates)
            .HasColumnType("nvarchar(20)");

        builder.Property(oo => oo.FixAssetMachineryType)
            .HasDefaultValue(FixAssetMachineryType.ownership)
            .IsRequired();

        builder.Property(oo => oo.MachineryPrice);

        builder.Property(oo => oo.HourlyRate);

        builder.Property(oo => oo.DailyRate);

        builder.Property(oo => oo.ServiceRate);

        builder.Property(oo => oo.VolumeRate);

        builder.Property(oo => oo.ContractorId);

        builder.Property(oo => oo.IsActive)
            .HasDefaultValue(true)
            .IsRequired();

        builder.Property(oo => oo.Description)
            .HasColumnType("nvarchar(1500)");

        builder.HasOne(oo => oo.Machinery)
               .WithMany(oo => oo.FixAssetMachineries)
               .HasForeignKey("MachineryId")
               .HasPrincipalKey(nameof(Machinery.Id))
               .OnDelete(DeleteBehavior.Restrict);
    }
}
