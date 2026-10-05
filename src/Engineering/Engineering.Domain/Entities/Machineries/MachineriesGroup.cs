
namespace Engineering.Domain.Entities.Machineries;

public class MachineriesGroup : ActivateEntity<MachineriesGroup, long>
{
    [Description(MachineriesCmts.GroupName)]
    public string GroupName { get; private set; } = string.Empty;
    [Description(MachineriesCmts.GroupCode)]
    public string GroupCode { get; private set; } = string.Empty;
    [Description(GlobalCmts.CompanyId)]
    public long? CompanyId { get; private set; }

    public MachineriesGroup(
        string groupName,
        string groupCode,
        bool isActive,
        long? companyId) : this()
    {
        SetName(groupName);
        SetCode(groupCode);
        SetCompanyId(companyId);
        if (isActive)
            SetActive();
        else
            SetDeactivate();
    }

    #region Set data

    public void SetName(string value)
    {
        GroupName = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }
    public void SetCode(string value)
    {
        GroupCode = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }

    public void SetCompanyId(long? value)
    {
        CompanyId = value;
    }
    #endregion

    #region Methods 

    public void AddMachinery(Machinery newData)
    {
        ArgumentNullException.ThrowIfNull(newData);
        if (_machineries.Any(oo => oo.MachineryName == newData.MachineryName || oo.MachineryCode == newData.MachineryCode && oo.Created == newData.Created))
            return;

        _machineries.Add(newData);
    }

    #endregion

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    [Description(GlobalCmts.Machineries)]
    private List<Machinery> _machineries;
    public IReadOnlyList<Machinery> Machineries => _machineries;
    private MachineriesGroup()
    {
        _machineries = [];
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
