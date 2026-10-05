using Engineering.Domain.Entities.RequestRewards;

namespace Engineering.Persistence.Configurations.RequestRewards;

public class RequestRewardProductConfiguration : IEntityTypeConfiguration<RequestRewardProduct>
{
    private const string _tableName = "RequestRewardProducts";
    public void Configure(EntityTypeBuilder<RequestRewardProduct> builder)
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

        builder.Property(oo => oo.ProductId)
            .IsRequired();

        builder.Property(oo => oo.CurrencyId)
            .IsRequired();

        builder.Property(oo => oo.Price)
            .HasColumnType("decimal(18, 2)")
            .IsRequired();

        builder.Property(oo => oo.Count)
            .IsRequired();


        builder.HasOne(oo => oo.RequestReward)
            .WithMany(oo => oo.RequestRewardProducts)
            .HasForeignKey("RequestRewardId")
            .HasPrincipalKey(nameof(RequestReward.Id))
            .OnDelete(DeleteBehavior.NoAction);

    }
}
