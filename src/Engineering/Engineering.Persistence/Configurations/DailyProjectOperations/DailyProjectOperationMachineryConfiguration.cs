using Engineering.Domain.Entities.DailyProjectOperations;
using Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Persistence.Configurations.DailyProjectOperations;

public class DailyProjectOperationMachineryConfiguration : IEntityTypeConfiguration<DailyProjectOperationMachinery>
{
    private const string _tableName = "DailyProjectOperationMachineries";
    public void Configure(EntityTypeBuilder<DailyProjectOperationMachinery> builder)
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

        builder.Property(oo => oo.FinalValue)
            .HasColumnType("bigint");

        builder.Property(oo => oo.UnusedValue)
            .HasColumnType("bigint");

        builder.Property(oo => oo.Number)
            .HasColumnType("decimal(18,5)");

        builder.HasOne(c => c.ConsumableVolumeMachinery)
               .WithMany(c => c.DailyOperationMachineries)
               .HasForeignKey("ConsumableVolumeMachineryId")
               .HasPrincipalKey(nameof(ConsumableVolumeMachinery.Id))
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.RequestMachinery)
               .WithMany(c => c.DailyMachineries)
               .HasForeignKey("RequestMachineryId")
               .HasPrincipalKey(nameof(RequestMachinery.Id))
               .OnDelete(DeleteBehavior.Restrict);
    }
}

