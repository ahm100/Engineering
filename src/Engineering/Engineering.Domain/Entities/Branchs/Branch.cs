namespace Engineering.Domain.Entities.Branchs;

[Description(GlobalCmts.Branch)]
public class Branch : ActivateEntity<Branch, long>
{

    [Description(BranchCmts.BranchName)]
    public string BranchName { get; private set; } = string.Empty;
    [Description(BranchCmts.BranchCode)]
    public string BranchCode { get; private set; } = string.Empty;
    [Description(GlobalCmts.PreferentialReferenceCode)]
    public Guid PreferentialReferenceCode { get; private set; }
    [Description(GlobalCmts.CompanyId)]
    public long? CompanyId { get; private set; }

    [Description(GlobalCmts.Category)]
    public long CategoryId { get; set; }
    public Category Category { get; set; }

    public Branch(
        Category category,
        string branchName,
        string branchCode,
        bool isActive,
        long? companyId) : this()
    {
        SetCategory(category);
        SetBranchName(branchName);
        SetBranchCode(branchCode);
        SetCompanyId(companyId);
        if (isActive)
            SetActive();
        else
            SetDeactivate();
        SetPreferentialReferenceCode(PreferentialReferenceCode);
    }

    #region Set data

    public void SetCategory(Category value)
    {
        Category = Guard.Against.Null(value, nameof(value));
        CategoryId = Guard.Against.Null(value.Id, nameof(value.Id));
    }
    public void SetPreferentialReferenceCode(Guid value)
    {
        PreferentialReferenceCode = Guard.Against.Null(value, nameof(value));
    }
    public void SetName(string value)
    {
        BranchName = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }
    public void SetCode(string value)
    {
        BranchCode = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }
    public void SetCompanyId(long? value)
    {
        CompanyId = value;
    }
    public void SetBranchName(string value)
    {
        BranchName = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }
    public void SetBranchCode(string value)
    {
        BranchCode = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }

    #endregion

    #region Methods 

    public void AddSeason(Season season)
    {
        ArgumentNullException.ThrowIfNull(season);
        if (_seasons.Any(oo => oo.SeasonName == season.SeasonName && oo.Created == season.Created))
            return;

        _seasons.Add(season);
    }

    #endregion


    public string GetPreferentialName()
    {
        if (BranchName.Contains(Category.CategoryName))
            return BranchName;

        return $"{BranchName}-{Category.CategoryName}";
    }

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    [Description(GlobalCmts.Season)]
    private List<Season> _seasons;
    public IReadOnlyList<Season> Seasons => _seasons.AsReadOnly();
    private Branch()
    {
        _seasons = new List<Season>();
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
