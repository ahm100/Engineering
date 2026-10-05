using Engineering.Domain.Entities.Projects.Junctions;

namespace Engineering.Domain.Entities.Categories;

[Description(GlobalCmts.Category)]
public class Category : ActivateEntity<Category, long>
{

    [Description(CategoryCmts.CategoryName)]
    public string CategoryName { get; private set; } = string.Empty;
    [Description(CategoryCmts.CategoryCode)]
    public string CategoryCode { get; private set; } = string.Empty;
    [Description(GlobalCmts.CompanyId)]
    public long? CompanyId { get; private set; }
    [Description(GlobalCmts.PreferentialReferenceCode)]
    public Guid PreferentialReferenceCode { get; private set; }

    public Category(
        string categoryName,
        string categoryCode,
        bool isActive,
        long? companyId) : this()
    {
        SetName(categoryName);
        SetCode(categoryCode);
        SetCompanyId(companyId);
        if (isActive)
            SetActive();
        else
            SetDeactivate();
        SetPreferentialReferenceCode(Guid.NewGuid());
    }

    #region Set data

    public void SetPreferentialReferenceCode(Guid value)
    {
        PreferentialReferenceCode = value;
    }
    public void SetName(string value)
    {
        CategoryName = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }
    public void SetCode(string value)
    {
        CategoryCode = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }
    public void SetCompanyId(long? value)
    {
        CompanyId = value;

    }

    #endregion

    #region Methods 

    public void AddBranch(Branch newData)
    {
        ArgumentNullException.ThrowIfNull(newData);
        if (_branchs.Any(oo => oo.BranchName == newData.BranchName || oo.BranchCode == newData.BranchCode && oo.Created == newData.Created))
            return;

        _branchs.Add(newData);
    }

    #endregion

    public string GetPreferentialName() => CategoryName;

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    [Description(GlobalCmts.Branch)]
    private List<Branch> _branchs;
    public IReadOnlyList<Branch> Branchs => _branchs;
    private readonly List<ProjectCategory> _projectCategory;
    public IReadOnlyList<ProjectCategory> ProjectCategories => _projectCategory;
    private Category()
    {
        _branchs = [];
        _projectCategory = [];
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
