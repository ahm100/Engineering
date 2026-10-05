namespace Engineering.Domain.Entities.CostCenters;

[Description(CCenterCmts.CostCenterType)]
public class CostCenterType : ActivateEntity<CostCenterType, long>
{

    [Description(CCenterCmts.CostCenterTypeCode)]
    public string CostCenterTypeCode { get; private set; } = string.Empty;
    [Description(CCenterCmts.CostCenterTypeTitle)]
    public string CostCenterTypeTitle { get; private set; } = string.Empty;
    [Description(CCenterCmts.CostCenterAuthorizedRole)]
    public long? CompanyId { get; private set; }
    public CostCenterType(
        string title,
        string code,
        bool isActive,
        long? companyId) : this()
    {
        SetName(title);
        SetCode(code);
        SetCompanyId(companyId);

        if (isActive)
            SetActive();
        else
            SetDeactivate();
    }

    #region Set data

    public void SetCompanyId(long? value)
    {
        CompanyId = value;
    }
    public void SetName(string value)
    {
        CostCenterTypeTitle = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }
    public void SetCode(string value)
    {
        CostCenterTypeCode = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }

    #endregion

    #region Methods 

    #endregion

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    [Description(GlobalCmts.CostCenter)]
    private readonly List<CostCenter> _costCenters;
    public IReadOnlyList<CostCenter> CostCenters => _costCenters;
    [Description(GlobalCmts.CostCenterHistory)]
    private readonly List<CostCenterHistory> _costCenterHistories;
    public IReadOnlyList<CostCenterHistory> CostCenterHistories => _costCenterHistories;
    private CostCenterType()
    {
        _costCenters = [];
        _costCenterHistories = [];
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}
