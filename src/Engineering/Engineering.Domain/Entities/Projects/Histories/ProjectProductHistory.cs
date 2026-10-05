namespace Engineering.Domain.Entities.Projects.Histories;

[Description(ProjectCmts.ProjectProductHistory)]
public class ProjectProductHistory : ActivateEntity<ProjectProductHistory, long>
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
    [Description(ProjectCmts.DefaultManagerSet)]
    public bool DefaultManagerSet { get; private set; } = false;

    [Description(ProjectCmts.ProjectProduct)]
    public long ProjectProductId { get; private set; }
    public ProjectProduct ProjectProduct { get; private set; }

    public ProjectProductHistory(
        ProjectProduct projectProduct) : this()
    {
        SetProjectProduct(projectProduct);
        SetProductGroupId(projectProduct.ProductGroupId);
        SetProductCategoryId(projectProduct.ProductCategoryId);
        SetRequestQuantity(projectProduct.RequestQuantity);
        SetRemainingQuantity(projectProduct.RemainingQuantity);
        SetTolerancePercentage(projectProduct.TolerancePercentage);
        SetDefaultManagerSet(projectProduct.DefaultManagerSet);
        if (projectProduct.IsActive)
            SetActive();
        else
            SetDeactivate();
    }

    #region Set data

    public void Update(
        decimal requestQuantity,
        decimal? tolerancePercentage)
    {
        SetRequestQuantity(requestQuantity);
        SetTolerancePercentage(tolerancePercentage ?? TolerancePercentage);
    }

    public void SetProjectProduct(ProjectProduct value)
    {
        ProjectProduct = Guard.Against.Null(value, nameof(value));
        ProjectProductId = Guard.Against.Null(value.Id, nameof(value.Id));
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

    public void SetRemainingQuantity(decimal value)
    {
        RemainingQuantity = Guard.Against.Null(value, nameof(value));
    }

    public void SetRequestQuantity(decimal value)
    {
        RequestQuantity = Guard.Against.Null(value, nameof(value));
    }
    #endregion

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private ProjectProductHistory() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}
