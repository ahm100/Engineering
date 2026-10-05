using Engineering.Domain.Entities.SessionRecords.Enums;

namespace Engineering.Domain.Entities.SessionRecords;

[Description(SessionRecordCmts.SessionRecordDoc)]
public class SessionRecordDoc : AuditableEntity<SessionRecordDoc, long>
{
    [Description(GlobalCmts.URL)]
    public string URL { get; private set; } = string.Empty;

    [Description(SessionRecordCmts.SessionRecordDocType)]
    public SessionRecordDocType Type { get; private set; }

    [Description(SessionRecordCmts.SessionRecord)]
    public long SessionRecordId { get; private set; }
    public SessionRecord SessionRecord { get; private set; } = null!;

    public SessionRecordDoc(
        string url,
        SessionRecordDocType type,
        SessionRecord sessionRecord) : this()
    {
        SetURL(url);
        SetType(type);
        SetSessionRecord(sessionRecord);
    }

    #region Commands

    public void SetData(string url, SessionRecordDocType type, SessionRecord sessionRecord)
    {
        SetURL(url);
        SetType(type);
        SetSessionRecord(sessionRecord);
    }

    public void SetURL(string value)
    {
        URL = Guard.Against.Null(value, nameof(value));
    }

    public void SetType(SessionRecordDocType value)
    {
        Type = value;
    }

    public void SetSessionRecord(SessionRecord value)
    {
        SessionRecord = Guard.Against.Null(value, nameof(value));
        SessionRecordId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    #endregion

    private SessionRecordDoc() { }
}