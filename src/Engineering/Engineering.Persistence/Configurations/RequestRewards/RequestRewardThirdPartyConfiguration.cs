using Engineering.Domain.Entities.RequestRewards;

namespace Engineering.Persistence.Configurations.RequestRewards;

public class RequestRewardThirdPartyConfiguration : IEntityTypeConfiguration<RequestRewardThirdParty>
{
    private const string _tableName = "RequestRewardThirdParties";
    public void Configure(EntityTypeBuilder<RequestRewardThirdParty> builder)
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


        builder.HasOne(oo => oo.RequestReward)
            .WithMany(oo => oo.RequestRewardThirdParties)
            .HasForeignKey("RequestRewardId")
            .HasPrincipalKey(nameof(RequestReward.Id))
            .OnDelete(DeleteBehavior.Restrict);

    }
}