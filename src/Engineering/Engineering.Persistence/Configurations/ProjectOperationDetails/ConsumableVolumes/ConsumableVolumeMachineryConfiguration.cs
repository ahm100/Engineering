using Engineering.Domain.Entities.Machineries;
using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes;

namespace Engineering.Persistence.Configurations.ProjectOperationDetails.ConsumableVolumes;

public class ConsumableVolumeMachineryConfiguration : IEntityTypeConfiguration<ConsumableVolumeMachinery>
{
    private const string _tableName = "ProjectOperationDetailConsumableVolumeMachineries";
    public void Configure(EntityTypeBuilder<ConsumableVolumeMachinery> builder)
    {
        builder.ToTable(_tableName);
        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.Number)
            .IsRequired();

        builder.Property(oo => oo.IsStandard)
            .IsRequired();

        builder.Property(oo => oo.StandardValue)
            .HasColumnType("bigint");

        builder.Property(oo => oo.FinalValue)
            .HasColumnType("decimal(18,3)")
            .IsRequired();

        builder.Property(oo => oo.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(oo => oo.Created)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(oo => oo.CreatorId)
            .IsRequired();

        builder.Property(oo => oo.Unit);

        builder.HasOne(oo => oo.ProjectOperationDetail)
            .WithMany(oo => oo.ConsumableVolumeMachineries)
            .HasForeignKey("ProjectOperationDetailId").HasPrincipalKey(nameof(ProjectOperationDetail.Id))
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(oo => oo.Machinery)
            .WithMany(oo => oo.ConsumableVolumeMachineries)
            .HasForeignKey("MachineryId").HasPrincipalKey(nameof(Machinery.Id))
            .OnDelete(DeleteBehavior.NoAction);

    }
}