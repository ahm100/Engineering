using Engineering.Domain.Entities.SessionRecords.Enums;

namespace Engineering.Domain.Entities.SessionRecords;

[Description(SessionRecordCmts.SessionRecordAction)]
public class SessionRecordAction : ActivateEntity<SessionRecordAction, long>
{
    [Description(SessionRecordCmts.SessionRecord)]
    public SessionRecord SessionRecord { get; private set; } = null!;
    public long SessionRecordId { get; private set; }

    [Description(GlobalCmts.Description)]
    public string Description { get; private set; } = string.Empty;

    [Description(SessionRecordCmts.Status)]
    public SessionRecordActionStatus Status { get; private set; }

    [Description(SessionRecordCmts.Deadline)]
    public DateOnly Deadline { get; private set; }

    [Description(SessionRecordCmts.UserId)]
    public long UserId { get; private set; }

    public SessionRecordAction(
        SessionRecord sessionRecord,
        string description,
        SessionRecordActionStatus status,
        DateOnly deadline,
        long userId) : this()
    {
        SetSessionRecord(sessionRecord);
        SetDescription(description);
        SetStatus(status);
        SetDeadline(deadline);
        SetUserId(userId);
    }

    public static SessionRecordAction Create(
        SessionRecord sessionRecord, string description, SessionRecordActionStatus status, DateOnly deadline, long userId)
            => new(sessionRecord, description, status, deadline, userId);

    #region Commands

    public void SetData(string description, SessionRecordActionStatus status, DateOnly deadline, long userId)
    {
        SetDescription(description);
        SetStatus(status);
        SetDeadline(deadline);
        SetUserId(userId);
    }

    public void SetDescription(string value)
    {
        Description = Guard.Against.Null(value, nameof(value));
    }
    public void SetStatus(SessionRecordActionStatus value)
    {
        Status = value;
    }
    public void SetDeadline(DateOnly value)
    {
        Deadline = value;
    }

    public void SetSessionRecord(SessionRecord value)
    {
        SessionRecord = Guard.Against.Null(value, nameof(value));
        SessionRecordId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetUserId(long value)
    {
        UserId = value;
    }

    #endregion

    private SessionRecordAction() { }
}