using Engineering.Domain.Entities.RequestRewards;

namespace Engineering.Persistence.Configurations.RequestRewards;

public class RequestRewardHistoryConfiguration : IEntityTypeConfiguration<RequestRewardHistory>
{
    private const string _tableName = "RequestRewardHistories";
    public void Configure(EntityTypeBuilder<RequestRewardHistory> builder)
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

        builder.Property(oo => oo.Description)
       .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.CurrencyId);

        builder.Property(oo => oo.Status)
            .IsRequired();

        builder.Property(oo => oo.ConfirmedPrice)
           .HasColumnType("decimal(18, 2)")
           .IsRequired();

        builder.Property(oo => oo.OfferedPrice)
          .HasColumnType("decimal(18, 2)")
          .IsRequired();

        builder.Property(oo => oo.RegistrationDate)
           .IsRequired();

        builder.Property(oo => oo.ManagerDescription)
           .HasColumnType("nvarchar(1500)");


        builder.HasOne(oo => oo.RequestReward)
            .WithMany(oo => oo.RequestRewardHistories)
            .HasForeignKey("RequestRewardId")
            .HasPrincipalKey(nameof(RequestReward.Id))
            .OnDelete(DeleteBehavior.Restrict);
    }
}
