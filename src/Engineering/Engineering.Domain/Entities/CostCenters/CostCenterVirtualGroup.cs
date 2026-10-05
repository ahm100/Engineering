namespace Engineering.Domain.Entities.CostCenters;

[Description(CCenterCmts.CostCenterVirtualGroup)]

public class CostCenterVirtualGroup : AuditableEntity<CostCenterVirtualGroup, long>
{

    #region Properties
    [Description(CCenterCmts.Title)]
    public string Title { get; private set; } = string.Empty;
    [Description(CCenterCmts.Link)]
    public string Link { get; private set; } = string.Empty;
    [Description(CCenterCmts.Identifier)]
    public string Identifier { get; private set; } = string.Empty;
    [Description(GlobalCmts.Description)]
    public string? Description { get; private set; } = string.Empty;
    [Description(CCenterCmts.SendToday)]
    public bool SendToday { get; private set; }
    [Description(CCenterCmts.TodayTime)]
    public TimeSpan? TodayTime { get; private set; }
    [Description(CCenterCmts.SendYesterday)]
    public bool SendYesterday { get; private set; }
    [Description(CCenterCmts.YesterdayTime)]
    public TimeSpan? YesterdayTime { get; private set; }
    [Description(GlobalCmts.CostCenterId)]
    public long CostCenterId { get; private set; }
    [Description(GlobalCmts.CostCenter)]
    public CostCenter CostCenter { get; private set; }
    [Description(CCenterCmts.CostCenterVirtualGroupAdmin)]
    private List<CostCenterVirtualGroupAdmin> _admins;
    public IReadOnlyList<CostCenterVirtualGroupAdmin> Admins => _admins;

    #endregion

    #region Constructors

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private CostCenterVirtualGroup() { _admins = []; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public CostCenterVirtualGroup(
        string title,
        string link,
        string identifier,
        string? description,
        bool sendToday, TimeSpan? todayTime,
        bool sendYesterday,
        TimeSpan? yesterdayTime,
        CostCenter costCenter) : this()
    {
        SetTitle(title);
        SetLink(link);
        SetIdentifier(identifier);
        SetLink(link);
        SetDescription(description);
        SetSendToday(sendToday);
        SetTodayTime(todayTime);
        SetSendYesterday(sendYesterday);
        SetYesterdayTime(yesterdayTime);
        SetCostCenter(costCenter);
    }

    #endregion

    #region Commands

    public void SetTitle(string value)
    {
        Title = Guard.Against.Null(value, nameof(value));
    }
    public void SetCostCenter(CostCenter value)
    {
        CostCenter = Guard.Against.Null(value, nameof(value));
        CostCenterId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetLink(string value)
    {
        Link = Guard.Against.Null(value, nameof(value));
    }

    public void SetIdentifier(string value)
    {
        Identifier = Guard.Against.Null(value, nameof(value));
    }

    public void SetDescription(string? value)
    {
        Description = value;
    }

    public void SetSendToday(bool value)
    {
        ArgumentNullException.ThrowIfNull(value);

        SendToday = Guard.Against.Null(value, nameof(value));
    }

    public void SetTodayTime(TimeSpan? value)
    {
        TodayTime = value;
    }

    public void SetSendYesterday(bool value)
    {
        ArgumentNullException.ThrowIfNull(value);

        SendYesterday = Guard.Against.Null(value, nameof(value));
    }

    public void SetYesterdayTime(TimeSpan? value)
    {
        YesterdayTime = value;
    }

    #endregion
}
