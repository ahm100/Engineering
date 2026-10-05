
namespace Engineering.Domain.Entities.Messengers;

public class MessengerChannelHistory : AuditableEntity<MessengerChannelHistory>
{
    public string Message { get; private set; }
    public string? ErrorMessage { get; private set; }
    public string? FileUrls { get; private set; }
    public bool IsSend { get; private set; }

    public long MessengerChannelId { get; private set; }
    public MessengerChannel MessengerChannel { get; private set; }

    public MessengerChannelHistory(
        MessengerChannel messengerChannel,
        string message,
        string? errorMessage,
        string? fileUrls,
        bool isSend) : this()
    {
        SetMessengerChannel(messengerChannel);
        SetMessage(message);
        SetErrorMessage(errorMessage);
        SetFileUrls(fileUrls);
        SetIsSend(isSend);
    }

    public void SetMessengerChannel(MessengerChannel messengerChannel)
    {
        MessengerChannel = Guard.Against.Null(messengerChannel, nameof(messengerChannel));
        MessengerChannelId = messengerChannel.Id;
    }

    public void SetMessage(string message)
    {
        Message = Guard.Against.NullOrEmpty(message, nameof(message));
    }

    public void SetErrorMessage(string? errorMessage)
    {
        ErrorMessage = errorMessage;
    }

    public void SetFileUrls(string? fileUrls)
    {
        FileUrls = fileUrls;
    }

    public void SetIsSend(bool isSend)
    {
        IsSend = isSend;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private MessengerChannelHistory()
    {

    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

}