using Engineering.Domain.Entities.FiduciaryProducts.Enums;
using Engineering.Domain.Entities.ProjectOperations;
using Engineering.Domain.Entities.Projects;

namespace Engineering.Domain.Entities.FiduciaryProducts;

/// <summary>
/// تاریخچه کالای امانی
/// </summary>
public class FiduciaryProductHistory : AuditableEntity<FiduciaryProductHistory>
{
    [Description(FiduciaryProductCmts.Project)]
    public long ProjectId { get; private set; }
    public Project Project { get; private set; }

    [Description(FiduciaryProductCmts.ProjectOperation)]
    public long ProjectOperationId { get; private set; }
    public ProjectOperation ProjectOperation { get; private set; }

    [Description(FiduciaryProductCmts.ThirdPartyId)]
    public long ThirdPartyId { get; private set; }

    [Description(FiduciaryProductCmts.Description)]
    public string? Description { get; private set; } = string.Empty;

    [Description(FiduciaryProductCmts.StatusDescription)]
    public string? StatusDescription { get; private set; } = string.Empty;

    [Description(FiduciaryProductCmts.LastDescription)]
    public string? LastDescription { get; private set; } = string.Empty;

    [Description(FiduciaryProductCmts.Status)]
    public FiduciaryProductStatus Status { get; private set; }

    [Description(FiduciaryProductCmts.FiduciaryProduct)]
    public long FiduciaryProductId { get; private set; }
    public FiduciaryProduct FiduciaryProduct { get; private set; }

    #region Constructors

    public FiduciaryProductHistory(
        Project project,
        ProjectOperation projectOperation,
        long thirdPartyId,
        string? description,
        FiduciaryProductStatus status,
        FiduciaryProduct fiduciaryProduct,
        string? statusDescription,
        string? lastDescription) : this()
    {
        Created = DateTime.Now;

        SetProject(project);
        SetProjectOperation(projectOperation);
        SetThirdPartyId(thirdPartyId);
        SetDescription(description);
        SetStatus(status);
        SetFiduciaryProduct(fiduciaryProduct);
        SetStatusDescription(statusDescription);
        SetLastDescription(lastDescription);
    }

    public FiduciaryProductHistory(
        Project project,
        ProjectOperation projectOperation,
        long thirdPartyId,
        string? description,
        FiduciaryProductStatus status,
        FiduciaryProduct fiduciaryProduct,
        long? userId,
        string? statusDescription,
        string? lastDescription) : this()
    {
        SetProject(project);
        SetProjectOperation(projectOperation);
        SetThirdPartyId(thirdPartyId);
        SetDescription(description);
        SetStatus(status);
        SetFiduciaryProduct(fiduciaryProduct);

        CreatorId = userId ?? 0;
        CheckUser = true;

        Created = DateTime.Now;

        SetStatusDescription(statusDescription);
        SetLastDescription(lastDescription);
    }

    #endregion

    #region Setters

    public void SetProject(Project project)
    {
        Project = Guard.Against.Null(project, nameof(project));
        ProjectId = Guard.Against.Null(project.Id, nameof(project.Id));
    }

    public void SetProjectOperation(ProjectOperation projectOperation)
    {
        ProjectOperation = Guard.Against.Null(projectOperation, nameof(projectOperation));
        ProjectOperationId = Guard.Against.Null(projectOperation.Id, nameof(projectOperation.Id));
    }
    public void SetThirdPartyId(long thirdPartyId)
    {
        ThirdPartyId = Guard.Against.NegativeOrZero(thirdPartyId, nameof(thirdPartyId));
    }
    public void SetDescription(string? description)
    {
        Description = description;
    }
    public void SetStatus(FiduciaryProductStatus status)
    {
        Status = Guard.Against.Null(status, nameof(status));
    }
    public void SetFiduciaryProduct(FiduciaryProduct fiduciaryProduct)
    {
        FiduciaryProduct = Guard.Against.Null(fiduciaryProduct, nameof(fiduciaryProduct));
        FiduciaryProductId = Guard.Against.Null(fiduciaryProduct.Id, nameof(fiduciaryProduct.Id));
    }
    public void SetStatusDescription(string? statusDescription)
    {
        StatusDescription = statusDescription;
    }
    public void SetLastDescription(string? lastDescription)
    {
        LastDescription = lastDescription;
    }
    #endregion

    #region Constructors

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private FiduciaryProductHistory() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    #endregion
}
