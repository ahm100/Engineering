using Engineering.Domain.Entities.Messengers;

namespace Engineering.Persistence.Configurations.Messengers;

public class MessengerChannelHistoryConfiguration : IEntityTypeConfiguration<MessengerChannelHistory>
{
    private const string _tableName = "MessengerChannelHistories";

    public void Configure(EntityTypeBuilder<MessengerChannelHistory> builder)
    {
        builder.ToTable(_tableName);
        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.FileUrls)
            .HasColumnType("nvarchar(max)");

        builder.Property(oo => oo.Message)
            .HasColumnType("nvarchar(2500)")
            .HasMaxLength(2500)
            .IsRequired();

        builder.Property(oo => oo.ErrorMessage)
            .HasColumnType("nvarchar(2500)")
            .HasMaxLength(2500)
            .IsRequired();

        builder.Property(oo => oo.IsSend)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(oo => oo.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(oo => oo.Created)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(oo => oo.CreatorId)
            .IsRequired();

        builder.HasOne(oo => oo.MessengerChannel)
            .WithMany(oo => oo.MessengerChannelHistories)
            .HasForeignKey("MessengerChannelId").HasPrincipalKey(nameof(MessengerChannel.Id))
            .OnDelete(DeleteBehavior.NoAction);
    }
}