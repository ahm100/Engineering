using Engineering.Domain.Entities.Messengers.Enums;

namespace Engineering.Domain.Entities.Messengers;

public class MessengerChannel : AuditableEntity<MessengerChannel>
{
    [Description(MessengerCmts.ChatId)]
    public string ChatId { get; private set; }

    [Description(MessengerCmts.ChatUrl)]
    public string? ChatUrl { get; private set; }

    [Description(MessengerCmts.ChatName)]
    public string? ChatName { get; private set; } = string.Empty;

    [Description(MessengerCmts.MessengerMessageType)]
    public MessengerMessageType MessengerMessageType { get; private set; }

    [Description(MessengerCmts.Messenger)]
    public long MessengerId { get; private set; }
    public Messenger Messenger { get; private set; }


    public MessengerChannel(
        Messenger messenger,
        MessengerMessageType messengerMessageType,
        string chatId,
        string? chatUrl,
        string? chatName
        ) : this()
    {
        SetMessenger(messenger);
        SetMessengerMessageType(messengerMessageType);
        SetChatId(chatId);
        SetChatUrl(chatUrl);
        SetChatName(chatName);
    }

    public void Update(
        MessengerMessageType? messengerMessageType,
        string? chatId,
        string? chatUrl,
        string? chatName
        )
    {
        SetMessengerMessageType(messengerMessageType ?? MessengerMessageType);
        SetChatId(chatId ?? ChatId);
        SetChatUrl(chatUrl ?? ChatUrl);
        SetChatName(chatName ?? ChatName);
    }

    public void AddHistory(
        string message,
        string? errorMessage,
        string? fileUrls,
        bool isSend)
    {
        _messengerChannelHistories.Add(new(this, message, errorMessage ?? "", fileUrls, isSend));
    }

    public void SetMessenger(
        Messenger value)
    {
        Messenger = Guard.Against.Null(value, nameof(value));
        MessengerId = value.Id;
    }

    public void SetMessengerMessageType(
        MessengerMessageType value)
    {
        MessengerMessageType = Guard.Against.Null(value, nameof(value));
    }

    public void SetChatId(string value)
    {
        ChatId = Guard.Against.NullOrEmpty(value, nameof(value));
    }

    public void SetChatUrl(string? value)
    {
        ChatUrl = value;
    }

    public void SetChatName(string? value)
    {
        ChatName = value;
    }

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private readonly List<MessengerChannelHistory> _messengerChannelHistories;
    public IReadOnlyList<MessengerChannelHistory> MessengerChannelHistories => _messengerChannelHistories;
    private MessengerChannel()
    {
        _messengerChannelHistories = [];
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}