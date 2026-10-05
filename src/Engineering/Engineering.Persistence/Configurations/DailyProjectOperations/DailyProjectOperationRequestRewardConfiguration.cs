using Engineering.Domain.Entities.DailyProjectOperations;
using Engineering.Domain.Entities.RequestRewards;

namespace Engineering.Persistence.Configurations.DailyProjectOperations;

public class DailyProjectOperationRequestRewardConfiguration : IEntityTypeConfiguration<DailyProjectOperationRequestReward>
{
    private const string _tableName = "DailyProjectOperationRequestRewards";
    public void Configure(EntityTypeBuilder<DailyProjectOperationRequestReward> builder)
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

        builder.HasOne(c => c.RequestReward)
            .WithMany(c => c.DailyProjectOperationRequestRewards)
            .HasForeignKey(c => c.RequestRewardId)
            .HasPrincipalKey(nameof(RequestReward.Id))
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.DailyProjectOperation)
            .WithMany(c => c.DailyProjectOperationRequestRewards)
            .HasForeignKey(c => c.DailyProjectOperationId)
            .HasPrincipalKey(nameof(DailyProjectOperation.Id))
            .OnDelete(DeleteBehavior.Restrict);

    }
}
