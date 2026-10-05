using Engineering.Domain.Entities.DailyProjectOperations;
using Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes;

namespace Engineering.Persistence.Configurations.DailyProjectOperations;

public class DailyProjectOperationExpertConfiguration : IEntityTypeConfiguration<DailyProjectOperationExpert>
{
    private const string _tableName = "DailyProjectOperationExperts";
    public void Configure(EntityTypeBuilder<DailyProjectOperationExpert> builder)
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

        builder.Property(oo => oo.ThirdPartyId)
            .IsRequired();

        builder.Property(oo => oo.FinalValue)
            .HasColumnType("bigint")
            .HasDefaultValue(0)
            .IsRequired();

        builder.Property(oo => oo.UnusedValue)
            .HasColumnType("bigint");

        builder.HasOne(c => c.ConsumableVolumeExpert)
               .WithMany(c => c.DailyOperationExperts)
               .HasForeignKey("ConsumableVolumeExpertId")
               .HasPrincipalKey(nameof(ConsumableVolumeExpert.Id))
               .OnDelete(DeleteBehavior.Restrict);
    }
}
