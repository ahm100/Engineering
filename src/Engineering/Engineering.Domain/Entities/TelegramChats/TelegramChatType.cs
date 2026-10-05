using Engineering.Domain.Entities.TelegramChats.Enums;

namespace Engineering.Domain.Entities.TelegramChats;

public class TelegramChatType : AuditableEntity<TelegramChatType>
{
    [Description(TelegramChatCmts.TelegramMessageType)]
    public TelegramMessageType TelegramMessageType { get; private set; }

    [Description(TelegramChatCmts.TelegramChat)]
    public TelegramChat TelegramChat { get; private set; }

    [Description(TelegramChatCmts.ChatName)]
    public string? ChatName { get; private set; } = string.Empty;

    [Description(TelegramChatCmts.ChatId)]
    public string ChatId { get; private set; }

    [Description(TelegramChatCmts.ChatUrl)]
    public string ChatUrl { get; private set; }

    public TelegramChatType(
        TelegramChat telegramChat,
        TelegramMessageType telegramMessageType,
        string chatId,
        string chatUrl,
        string? chatName) : this()
    {
        SetChatId(chatId);
        SetChatUrl(chatUrl);
        SetChatName(chatName);
        SetTelegramChat(telegramChat);
        SetTelegramMessageType(telegramMessageType);
    }

    public static TelegramChatType Create(
        TelegramChat telegramChat,
        TelegramMessageType telegramMessageType,
        string chatId,
        string chatUrl,
        string? chatName)
    {
        return new TelegramChatType(telegramChat, telegramMessageType, chatId, chatUrl, chatName);
    }

    #region Set data

    public void SetTelegramChat(TelegramChat value)
    {
        TelegramChat = Guard.Against.Null(value, nameof(value));
    }

    public void SetTelegramMessageType(TelegramMessageType value)
    {
        TelegramMessageType = Guard.Against.Null(value, nameof(value));
    }

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    public void SetChatUrl(string value)
    {
        ChatUrl = value;
    }

    public void SetChatId(string value)
    {
        ChatId = value;
    }

    public void SetChatName(string? value)
    {
        ChatName = value;
    }

    #endregion

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private TelegramChatType()
    {
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}