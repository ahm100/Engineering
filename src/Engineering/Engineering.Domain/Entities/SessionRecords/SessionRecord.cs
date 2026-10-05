using Engineering.Domain.Entities.Contracts;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.SessionRecords.Enums;

namespace Engineering.Domain.Entities.SessionRecords;

[Description(SessionRecordCmts.SessionRecord)]
public class SessionRecord : ActivateEntity<SessionRecord, long>
{
    [Description(GlobalCmts.TitleFa)]
    public string TitleFa { get; private set; } = string.Empty;

    [Description(GlobalCmts.TitleEn)]
    public string TitleEn { get; private set; } = string.Empty;


    [Description(ProjectCmts.Project)]
    public Project Project { get; set; }
    public long? ProjctId { get; set; }

    [Description(ContractCmts.Contract)]
    public Contract? Contract { get; set; }
    public long? ContractId { get; set; }

    [Description(SessionRecordCmts.ProjectName)]
    public string? ProjectName { get; private set; }

    [Description(SessionRecordCmts.SessionCategory)]
    public SessionCategory SessionCategory { get; set; }

    [Description(SessionRecordCmts.SessionType)]
    public SessionType SessionType { get; set; }

    [Description(ContractCmts.ContractNumber)]
    public long? ContractNumber { get; private set; }

    [Description(SessionRecordCmts.SessionDate)]
    public DateOnly SessionDate { get; private set; }

    [Description(SessionRecordCmts.StartTime)]
    public TimeOnly StartTime { get; private set; }

    [Description(SessionRecordCmts.EndTime)]
    public TimeOnly EndTime { get; private set; }

    [Description(SessionRecordCmts.Location)]
    public string? Location { get; private set; }

    public SessionRecord(
        string titleFa,
        string? titleEn,
        string? projectName,
        long? contractNumber,
        DateOnly sessionDate,
        TimeOnly startTime,
        TimeOnly endTime,
        string? location,
        SessionCategory category,
        SessionType type) : this()
    {
        SetTitleFa(titleFa);
        SetTitleEn(titleEn);
        SetProjectName(projectName);
        SetContractNumber(contractNumber);
        SetSessionDate(sessionDate);
        SetStartTime(startTime);
        SetEndTime(endTime);
        SetLocation(location);
        SetSessionCategory(category);
        SetSessionType(type);
    }

    public static SessionRecord Create(
        string titleFa,
        string? titleEn,
        string? projectName,
        long? contractNum,
        DateOnly sessionDate,
        TimeOnly startTime,
        TimeOnly endTime,
        string? location,
        SessionCategory category,
        SessionType type)
            => new(titleFa, titleEn, projectName, contractNum, sessionDate, startTime, endTime, location, category, type);

    #region Set data

    public void SetTitleFa(string value)
    {
        TitleFa = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }

    public void SetTitleEn(string? value)
    {
        TitleEn = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }

    public void SetProjectName(string? value)
    {
        ProjectName = value;
    }
    public void SetContractNumber(long? value)
    {
        ContractNumber = value;
    }
    public void SetSessionDate(DateOnly value)
    {
        SessionDate = value;
    }
    public void SetStartTime(TimeOnly value)
    {
        StartTime = value;
    }
    public void SetEndTime(TimeOnly value)
    {
        EndTime = value;
    }
    public void SetLocation(string? value)
    {
        Location = value;
    }
    public void SetSessionType(SessionType value)
    {
        SessionType = value;
    }

    public void SetSessionCategory(SessionCategory value)
    {
        SessionCategory = value;
    }

    #endregion

    #region Methods

    public void AddInvitee(SessionInvitee newData)
    {
        ArgumentNullException.ThrowIfNull(newData);
        _sessionInvitees.Add(newData);
    }

    public void AddItem(SessionItem newData)
    {
        ArgumentNullException.ThrowIfNull(newData);
        _sessionItems.Add(newData);
    }

    public void AddAction(SessionRecordAction newData)
    {
        ArgumentNullException.ThrowIfNull(newData);
        _sessionRecordActions.Add(newData);
    }

    public void AddDocument(SessionRecordDoc newData)
    {
        ArgumentNullException.ThrowIfNull(newData);
        _sessionRecordDocs.Add(newData);
    }

    #endregion

#pragma warning disable CS8618
    [Description(SessionRecordCmts.SessionInvitee)]
    private List<SessionInvitee> _sessionInvitees;
    public IReadOnlyList<SessionInvitee> SessionInvitees => _sessionInvitees;

    [Description(SessionRecordCmts.SessionItem)]
    private List<SessionItem> _sessionItems;
    public IReadOnlyList<SessionItem> SessionItems => _sessionItems;

    [Description(SessionRecordCmts.SessionRecordAction)]
    private List<SessionRecordAction> _sessionRecordActions;
    public IReadOnlyList<SessionRecordAction> SessionRecordActions => _sessionRecordActions;

    [Description(SessionRecordCmts.SessionRecordDoc)]
    private List<SessionRecordDoc> _sessionRecordDocs;
    public IReadOnlyList<SessionRecordDoc> SessionRecordDocs => _sessionRecordDocs;

    private SessionRecord()
    {
        _sessionInvitees = [];
        _sessionItems = [];
        _sessionRecordActions = [];
        _sessionRecordDocs = [];
    }
#pragma warning restore CS8618
}