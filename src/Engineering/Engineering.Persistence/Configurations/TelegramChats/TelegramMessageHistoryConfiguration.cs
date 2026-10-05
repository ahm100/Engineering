using Engineering.Domain.Entities.TelegramChats;

namespace Engineering.Persistence.Configurations.TelegramChats;

public class TelegramMessageHistoryConfiguration : IEntityTypeConfiguration<TelegramMessageHistory>
{
    private const string _tableName = "TelegramMessageHistories";

    public void Configure(EntityTypeBuilder<TelegramMessageHistory> builder)
    {
        builder.ToTable(_tableName);
        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.TelegramMessageType)
            .IsRequired();

        builder.Property(oo => oo.FileUrls)
            .HasColumnType("nvarchar(max)");

        builder.Property(oo => oo.Message)
            .HasColumnType("nvarchar(1500)")
            .HasMaxLength(1500)
            .IsRequired();

        builder.Property(oo => oo.ErrorMessage)
            .HasColumnType("nvarchar(1500)")
            .HasMaxLength(1500)
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

        builder.Property(oo => oo.ChatId)
            .HasColumnType("nvarchar(250)")
            .HasMaxLength(100)
            .IsRequired(false);

        builder.HasOne(oo => oo.TelegramChat)
            .WithMany(oo => oo.TelegramMessageHistorys)
            .HasForeignKey("TelegramChatId").HasPrincipalKey(nameof(TelegramChat.Id))
            .OnDelete(DeleteBehavior.NoAction);
    }
}