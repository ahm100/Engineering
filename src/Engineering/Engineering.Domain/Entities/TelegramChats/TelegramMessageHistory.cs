using Engineering.Domain.Entities.TelegramChats.Enums;

namespace Engineering.Domain.Entities.TelegramChats;

[Description(GlobalCmts.Histories)]
public class TelegramMessageHistory : AuditableEntity<TelegramChat>
{
    [Description(TelegramChatCmts.TelegramChat)]
    public long TelegramChatId { get; private set; }
    public TelegramChat TelegramChat { get; private set; }

    [Description(TelegramChatCmts.Message)]
    public string Message { get; private set; }

    [Description(TelegramChatCmts.ErrorMessage)]
    public string? ErrorMessage { get; private set; }

    [Description(TelegramChatCmts.FileUrls)]
    public string? FileUrls { get; private set; }

    [Description(TelegramChatCmts.IsSend)]
    public bool IsSend { get; private set; }

    [Description(TelegramChatCmts.TelegramMessageType)]
    public TelegramMessageType TelegramMessageType { get; private set; }

    [Description(TelegramChatCmts.ChatId)]
    public string? ChatId { get; private set; } = string.Empty;

    public TelegramMessageHistory(
        long telegramChatId,
        string message,
        string? errorMessage,
        string? fileUrls,
        TelegramMessageType telegramMessageType,
        string? chatId,
        bool isSend) : this()
    {
        SetTelegramChatId(telegramChatId);
        SetMessages(message);
        SetErrorMessage(errorMessage);
        SetFileUrls(fileUrls);
        SetTelegramMessageType(telegramMessageType);
        SetIsSend(isSend);
        SetChatId(chatId);
    }

    public static TelegramMessageHistory Create(
        long telegramChatId,
        string message,
        string? errorMessage,
        string? fileUrls,
        TelegramMessageType telegramMessageType,
        string? chatId,
        bool isSend)
    {
        return new TelegramMessageHistory(telegramChatId, message, errorMessage, fileUrls, telegramMessageType, chatId, isSend);
    }

    #region Set data

    public void SetTelegramChatId(long value)
    {
        TelegramChatId = Guard.Against.Null(value, nameof(value));
    }

    public void SetChatId(string? value)
    {
        ChatId = value;
    }

    public void SetMessages(string value)
    {
        Message = Guard.Against.Null(value, nameof(value));
    }

    public void SetErrorMessage(string? value)
    {
        ErrorMessage = value;
    }

    public void SetFileUrls(string? value)
    {
        FileUrls = value;
    }

    public void SetTelegramMessageType(TelegramMessageType value)
    {
        TelegramMessageType = Guard.Against.Null(value, nameof(value));
    }

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    public void SetIsSend()
    {
        IsSend = true;
    }

    public void SetIsSend(bool value)
    {
        IsSend = Guard.Against.Null(value, nameof(value));
    }

    #endregion

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private TelegramMessageHistory()
    {

    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

}