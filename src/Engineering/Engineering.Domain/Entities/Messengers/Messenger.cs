using Engineering.Domain.Entities.Messengers.Enums;

namespace Engineering.Domain.Entities.Messengers;

[Description(MessengerCmts.Messenger)]
public class Messenger : ActivateEntity<Messenger>
{
    [Description(MessengerCmts.MessengerType)]
    public MessengerType MessengerType { get; set; }

    [Description(MessengerCmts.MessengerTargetType)]
    public MessengerTargetType MessengerTargetType { get; set; }

    [Description(MessengerCmts.TargetId)]
    public long TargetId { get; set; }

    [Description(GlobalCmts.Description)]
    public string? Description { get; private set; } = string.Empty;

    [Description(GlobalCmts.CompanyId)]
    public long CompanyId { get; set; }


    public Messenger(
        MessengerType messengerType,
        MessengerTargetType messengerTargetType,
        long targetId,
        string? description,
        bool isActive,
        long companyId) : this()
    {
        SetMessengerType(messengerType);
        SetMessengerTargetType(messengerTargetType);
        SetTargetId(targetId);
        SetDescription(description);
        IsActive = isActive;
        SetCompanyId(companyId);
    }
    public void Update(
        MessengerType? messengerType,
        MessengerTargetType? messengerTargetType,
        long? targetId,
        string? description,
        bool? isActive)
    {
        SetMessengerType(messengerType ?? MessengerType);
        SetMessengerTargetType(messengerTargetType ?? MessengerTargetType);
        SetTargetId(targetId ?? TargetId);
        SetDescription(description ?? Description);
        IsActive = isActive ?? IsActive;
    }

    public void SetMessengerType(MessengerType messengerType)
    {
        MessengerType = Guard.Against.Null(messengerType, nameof(messengerType));
    }

    public void SetMessengerTargetType(MessengerTargetType messengerTargetType)
    {
        MessengerTargetType = Guard.Against.Null(messengerTargetType, nameof(messengerTargetType));
    }

    public void SetTargetId(long targetId)
    {
        TargetId = Guard.Against.NegativeOrZero(targetId, nameof(targetId));
    }

    public void SetDescription(string? description)
    {
        Description = description;
    }

    public void SetIsActive(bool isActive)
    {
        IsActive = isActive;
    }

    public void SetCompanyId(long companyId)
    {
        CompanyId = Guard.Against.NegativeOrZero(companyId, nameof(companyId));
    }

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private List<MessengerChannel> _messengerChannels;
    public IReadOnlyList<MessengerChannel> MessengerChannels => _messengerChannels;
    private Messenger()
    {
        _messengerChannels = [];
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}