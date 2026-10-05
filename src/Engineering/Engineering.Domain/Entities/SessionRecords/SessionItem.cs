namespace Engineering.Domain.Entities.SessionRecords;

[Description(SessionRecordCmts.SessionItem)]
public class SessionItem : AuditableEntity<SessionItem, long>
{
    [Description(SessionRecordCmts.SessionRecord)]
    public SessionRecord SessionRecord { get; private set; } = null!;
    public long SessionRecordId { get; private set; }

    [Description(GlobalCmts.Description)]
    public string Descriotion { get; private set; } = string.Empty;

    public SessionItem(SessionRecord sessionRecord, string descriotion) : this()
    {
        SetSessionRecord(sessionRecord);
        SetDescriotion(descriotion);
    }

    public static SessionItem Create(SessionRecord sessionRecord, string descriotion)
        => new(sessionRecord, descriotion);

    #region Commands

    public void SetData(SessionRecord sessionRecord, string descriotion)
    {
        SetSessionRecord(sessionRecord);
        SetDescriotion(descriotion);
    }

    public void SetDescriotion(string value) => Descriotion = Guard.Against.Null(value, nameof(value));

    public void SetSessionRecord(SessionRecord value)
    {
        SessionRecord = Guard.Against.Null(value, nameof(value));
        SessionRecordId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    #endregion

    private SessionItem() { }
}