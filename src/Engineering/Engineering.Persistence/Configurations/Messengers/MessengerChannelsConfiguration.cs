using Engineering.Domain.Entities.Messengers;

namespace Engineering.Persistence.Configurations.Messengers;

public class CostCenterMessengerChannelsConfiguration : IEntityTypeConfiguration<MessengerChannel>
{
    private const string _tableName = "MessengerChannels";
    public void Configure(EntityTypeBuilder<MessengerChannel> builder)
    {
        builder.ToTable(_tableName);
        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.ChatId)
            .HasColumnType("nvarchar(250)")
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(oo => oo.ChatName)
            .HasColumnType("nvarchar(250)")
            .HasMaxLength(250);

        builder.Property(oo => oo.ChatUrl)
            .HasColumnType("nvarchar(250)")
            .HasMaxLength(250);

        builder.Property(oo => oo.MessengerMessageType)
            .IsRequired();

        builder.Property(oo => oo.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(oo => oo.Created)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(oo => oo.CreatorId)
            .IsRequired();


        builder.HasOne(oo => oo.Messenger)
            .WithMany(oo => oo.MessengerChannels)
            .HasForeignKey("MessengerId").HasPrincipalKey(nameof(Messenger.Id))
            .OnDelete(DeleteBehavior.NoAction);

    }
}