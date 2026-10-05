using Engineering.Domain.Entities.SessionRecords.Enums;

namespace Engineering.Domain.Entities.SessionRecords;

[Description(SessionRecordCmts.SessionInvitee)]
public class SessionInvitee : AuditableEntity<SessionInvitee, long>
{
    [Description(SessionRecordCmts.SessionRecord)]
    public SessionRecord SessionRecord { get; private set; } = null!;
    public long SessionRecordId { get; private set; }

    [Description(SessionRecordCmts.Status)]
    public SessionInviteeStatus Status { get; private set; }

    [Description(GlobalCmts.CompanyId)]
    public long? CompanyId { get; private set; }

    [Description(SessionRecordCmts.UserId)]
    public long? UserId { get; private set; }

    public SessionInvitee(
        SessionRecord sessionRecord,
        SessionInviteeStatus status,
        long? companyId,
        long? userId) : this()
    {
        SetSessionRecord(sessionRecord);
        SetStatus(status);
        SetCompanyId(companyId);
        SetUserId(userId);
    }

    public static SessionInvitee Create(
        SessionRecord sessionRecord, SessionInviteeStatus status, long? companyId, long? userId)
            => new(sessionRecord, status, companyId, userId);

    #region Commands

    public void SetData(SessionInviteeStatus status, long? companyId, long? userId)
    {
        SetStatus(status);
        SetCompanyId(companyId);
        SetUserId(userId);
    }

    public void SetStatus(SessionInviteeStatus value)
    {
        Status = value;
    }

    public void SetCompanyId(long? value)
    {
        CompanyId = value;
    }

    public void SetUserId(long? value)
    {
        UserId = value;
    }

    public void SetSessionRecord(SessionRecord value)
    {
        SessionRecord = Guard.Against.Null(value, nameof(value));
        SessionRecordId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    #endregion

    private SessionInvitee() { }
}