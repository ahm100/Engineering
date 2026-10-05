using Engineering.Domain.Entities.FiduciaryProducts.Enums;
using Engineering.Domain.Entities.ProjectOperations;
using Engineering.Domain.Entities.Projects;

namespace Engineering.Domain.Entities.FiduciaryProducts;

[Description(FiduciaryProductCmts.FiduciaryProduct)]
public class FiduciaryProduct : AuditableEntity<FiduciaryProduct>
{
    #region Properties
    [Description(GlobalCmts.RequestNumber)]
    public long? RequestNumber { get; set; }

    [Description(GlobalCmts.Project)]
    public long ProjectId { get; private set; }
    public Project Project { get; private set; }

    [Description(GlobalCmts.ProjectOperation)]
    public long ProjectOperationId { get; private set; }
    public ProjectOperation ProjectOperation { get; private set; }

    [Description(GlobalCmts.ThirdParty)]
    public long ThirdPartyId { get; private set; }

    [Description(GlobalCmts.Description)]
    public string? Description { get; private set; } = string.Empty;

    [Description(FiduciaryProductCmts.StatusDescription)]
    public string? StatusDescription { get; private set; } = string.Empty;

    [Description(FiduciaryProductCmts.LastDescription)]
    public string? LastDescription { get; private set; } = string.Empty;

    [Description(GlobalCmts.Status)]
    public FiduciaryProductStatus Status { get; private set; } = FiduciaryProductStatus.New;

    [Description(GlobalCmts.CompanyId)]
    public long? CompanyId { get; private set; }

    #endregion

    public FiduciaryProduct(Project project,
        ProjectOperation projectOperation,
        long thirdPartyId,
        string? description,
        long? companyId) : this()
    {
        SetProject(project);
        SetProjectOperation(projectOperation);
        SetThirdPartyId(thirdPartyId);
        SetDescription(description);
        SetCompanyId(companyId);

        AddHistory();
    }

    #region Constructors

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private List<FiduciaryProductHistory> _histories;
    public IReadOnlyList<FiduciaryProductHistory> Histories => _histories;

    private List<FiduciaryProductDetail> _details;
    public IReadOnlyList<FiduciaryProductDetail> Details => _details;

    private FiduciaryProduct()
    {
        _histories = [];
        _details = [];
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    #endregion

    #region Commands
    public void SetCompanyId(long? value)
    {
        CompanyId = value;
    }

    public void AddHistory()
    {
        _histories.Add(new FiduciaryProductHistory(Project, ProjectOperation, ThirdPartyId, Description, Status, this, StatusDescription, LastDescription));
    }

    public void AddHistory(long? userId)
    {
        _histories.Add(new FiduciaryProductHistory(Project, ProjectOperation, ThirdPartyId, Description, Status, this, userId, StatusDescription, LastDescription));
    }

    public void ChangeStatus(FiduciaryProductStatus status)
    {
        ArgumentNullException.ThrowIfNull(status);
        Status = status;
        AddHistory();
    }

    public void ChangeStatusWithUser(FiduciaryProductStatus status, long? userId)
    {
        ArgumentNullException.ThrowIfNull(status);
        Status = status;
        AddHistory(userId);
    }

    public void ChangeStatusWithUser(FiduciaryProductStatus status, string? statusDescription, string? lastDescription, long? userId)
    {
        ArgumentNullException.ThrowIfNull(status);
        StatusDescription = statusDescription;
        LastDescription = lastDescription;
        Status = status;
        AddHistory(userId);
    }

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    public void SetProject(Project project)
    {
        ArgumentNullException.ThrowIfNull(project);
        Project = project;
    }

    public void SetProjectOperation(ProjectOperation projectOperation)
    {
        ArgumentNullException.ThrowIfNull(projectOperation);
        ProjectOperation = projectOperation;
    }

    public void SetThirdPartyId(long thirdPartyId)
    {
        ArgumentNullException.ThrowIfNull(thirdPartyId);
        ThirdPartyId = thirdPartyId;
    }

    public void SetDescription(string? description)
    {
        Description = description;
    }

    public void SetStatusDescription(string? description)
    {
        StatusDescription = description;
    }

    public void SetLastDescription(string? description)
    {
        LastDescription = description;
    }

    #endregion
}
