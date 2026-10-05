using Engineering.Domain.Entities.TelegramChats;

namespace Engineering.Persistence.Configurations.TelegramChats;

public class CostCenterTelegramChatTypesConfiguration : IEntityTypeConfiguration<TelegramChatType>
{
    private const string _tableName = "TelegramChatTypes";
    public void Configure(EntityTypeBuilder<TelegramChatType> builder)
    {
        builder.ToTable(_tableName);
        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.ChatName)
            .HasColumnType("nvarchar(250)")
            .HasMaxLength(250)
            .IsUnicode(true);

        builder.Property(oo => oo.ChatUrl)
            .HasColumnType("nvarchar(250)")
            .HasMaxLength(250)
            .IsUnicode(true)
            .IsRequired();

        builder.Property(oo => oo.ChatId)
            .HasColumnType("nvarchar(250)")
            .HasMaxLength(100)
            .IsUnicode(true)
            .IsRequired();

        builder.Property(oo => oo.TelegramMessageType)
            .IsRequired();

        builder.Property(oo => oo.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(oo => oo.Created)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(oo => oo.CreatorId)
            .IsRequired();


        builder.HasOne(oo => oo.TelegramChat)
            .WithMany(oo => oo.TelegramChatTypes)
            .HasForeignKey("TelegramChatId").HasPrincipalKey(nameof(TelegramChat.Id))
            .OnDelete(DeleteBehavior.NoAction);

    }
}