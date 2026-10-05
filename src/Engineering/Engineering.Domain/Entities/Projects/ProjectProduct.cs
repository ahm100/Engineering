using Engineering.Domain.Entities.Projects.Enums;
using Engineering.Domain.Entities.Projects.Histories;
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Domain.Entities.Projects;

[Description(ProjectCmts.ProjectProduct)]
public class ProjectProduct : ActivateEntity<ProjectProduct, long>
{
    [Description(ProjectCmts.TotalQuantity)]
    public decimal RequestQuantity { get; private set; } = 0;
    [Description(ProjectCmts.RemainingQuantity)]
    public decimal RemainingQuantity { get; private set; } = 0;
    [Description(ProjectCmts.CompletedQuantity)]
    public decimal CompletedQuantity { get; private set; } = 0;
    [Description(ProjectCmts.InProgressQuantity)]
    public decimal InProgressQuantity { get; private set; } = 0;
    [Description(ProjectCmts.ProductGroupId)]
    public long? ProductGroupId { get; private set; }
    [Description(ProjectCmts.ProductCategoryId)]
    public long? ProductCategoryId { get; private set; }
    [Description(ProjectCmts.TolerancePercentage)]
    public decimal TolerancePercentage { get; private set; } = 0;
    [Description(ProjectCmts.ProjectProductType)]
    public ProjectProductType ProjectProductType { get; private set; } = ProjectProductType.ProductGroup;
    [Description(GlobalCmts.CompanyId)]
    public long? CompanyId { get; private set; }
    [Description(ProjectCmts.DefaultManagerSet)]
    public bool DefaultManagerSet { get; private set; } = false;

    [Description(GlobalCmts.Project)]
    public long ProjectId { get; private set; }
    public Project Project { get; private set; }

    public ProjectProduct(
        Project project,
        decimal requestQuantity,
        long? productGroupId,
        long? productCategoryId,
        decimal tolerancePercentage,
        ProjectProductType projectProductType,
        bool? isActive,
        bool? defaultManagerSet,
        long? companyId) : this()
    {
        SetProject(project);
        SetProductGroupId(productGroupId);
        SetProductCategoryId(productCategoryId);
        SetRequestQuantity(requestQuantity);
        SetRemainingQuantity(requestQuantity);
        SetTolerancePercentage(tolerancePercentage);
        SetProjectProductType(projectProductType);
        SetCompanyId(companyId);
        SetDefaultManagerSet(defaultManagerSet);
        if (isActive.HasValue)
        {
            if (isActive.Value == true)
                SetActive();
            if (isActive.Value == false)
                SetDeactivate();
        }
        AddHistory();
    }

    #region Set data

    public void Update(
        decimal requestQuantity,
        bool? defaultManagerSet,
        decimal? tolerancePercentage)
    {
        var delta = RequestQuantity - RemainingQuantity;
        SetRequestQuantity(requestQuantity);
        SetDefaultManagerSet(defaultManagerSet ?? DefaultManagerSet);
        SetTolerancePercentage(tolerancePercentage ?? TolerancePercentage);
        SetRemainingQuantity(requestQuantity - delta);
        AddHistory();
    }

    public void SetTolerancePercentage(decimal value)
    {
        TolerancePercentage = Guard.Against.Null(value, nameof(value));
    }

    public void UpdateQuantities(
        decimal completedQuantity,
        decimal inProgressQuantity)
    {
        CompletedQuantity = Guard.Against.Null(completedQuantity, nameof(completedQuantity));
        InProgressQuantity = Guard.Against.Null(inProgressQuantity, nameof(inProgressQuantity));
        RemainingQuantity = RequestQuantity - completedQuantity - InProgressQuantity;
        AddHistory();
    }

    public void SetProject(Project value)
    {
        Project = Guard.Against.Null(value, nameof(value));
        ProjectId = Guard.Against.Null(value.Id, nameof(value));
    }

    public void SetProductGroupId(long? value)
    {
        ProductGroupId = value;
    }

    public void SetDefaultManagerSet(bool? value)
    {
        DefaultManagerSet = value ?? false;
    }

    public void SetProductCategoryId(long? value)
    {
        ProductCategoryId = value;
    }

    public void SetProjectProductType(ProjectProductType value)
    {
        ProjectProductType = Guard.Against.Null(value, nameof(value));
    }

    public void SetRemainingQuantity(decimal value)
    {
        RemainingQuantity = Guard.Against.Null(value, nameof(value));
    }

    public void SetRequestQuantity(decimal value)
    {
        RequestQuantity = Guard.Against.Null(value, nameof(value));
    }

    public void SetCompanyId(long? value)
    {
        CompanyId = value;
    }

    public void AddHistory()
    {
        _projectProductHistories.Add(new(this));
    }


    #endregion

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private readonly List<RequestGoodsSupplyDetail> _requestGoodsSupplyDetails;
    public IReadOnlyList<RequestGoodsSupplyDetail> RequestGoodsSupplyDetails => _requestGoodsSupplyDetails;

    private readonly List<ProjectProductHistory> _projectProductHistories;
    public IReadOnlyList<ProjectProductHistory> ProjectProductHistories => _projectProductHistories;

    private List<RequestGoodsSupplyTypeDetail> _requestGoodsSupplyTypeDetails;
    public IReadOnlyList<RequestGoodsSupplyTypeDetail> RequestGoodsSupplyTypeDetails => _requestGoodsSupplyTypeDetails;
    private ProjectProduct()
    {
        _requestGoodsSupplyDetails = [];
        _projectProductHistories = [];
        _requestGoodsSupplyTypeDetails = [];
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}
