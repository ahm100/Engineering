using Engineering.Domain.Entities.Machineries;
using Engineering.Domain.Entities.OperationInfos;
using Engineering.Domain.Entities.OperationInfos.ConsumptionStandards;

namespace Engineering.Persistence.Configurations.OperationInfos.ConsumptionStandards;

public class ConsumptionStandardMachineryConfiguration : IEntityTypeConfiguration<ConsumptionStandardMachinery>
{
    private const string TableName = "EngineeringStandardMachineries";
    public void Configure(EntityTypeBuilder<ConsumptionStandardMachinery> builder)
    {
        builder.ToTable(TableName);
        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.MachineryNumber)
                .HasColumnType("decimal(18,5)");

        builder.Property(oo => oo.TimeSpant)
            .IsRequired();

        builder.Property(oo => oo.UnusedPercentage)
            .HasColumnType("decimal(18,5)");

        builder.Property(oo => oo.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(oo => oo.Created)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(oo => oo.CreatorId)
            .IsRequired();


        builder.HasOne(oo => oo.OperationInfo)
            .WithMany(oo => oo.ConsumptionStandardMachineries)
            .HasForeignKey("OperationInfoId").HasPrincipalKey(nameof(OperationInfo.Id))
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(oo => oo.Machinery)
            .WithMany(oo => oo.ConsumptionStandardMachineries)
            .HasForeignKey("MachineryId").HasPrincipalKey(nameof(Machinery.Id))
            .OnDelete(DeleteBehavior.NoAction);

    }
}