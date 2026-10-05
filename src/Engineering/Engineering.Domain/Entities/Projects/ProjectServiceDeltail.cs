using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Domain.Entities.Projects;

[Description(ProjectCmts.ProjectServiceDetail)]
public class ProjectServiceDetail : AuditableEntity<ProjectServiceDetail>
{
    [Description(ProjectCmts.ProjectService)]
    public long ProjectServiceId { get; set; }
    public ProjectService ProjectService { get; set; }

    [Description(ProjectCmts.OperationInfoService)]
    public long OperationInfoServiceId { get; set; }
    public OperationInfoService OperationInfoService { get; set; }

    public ProjectServiceDetail(ProjectService projectService,
        OperationInfoService operationInfoService) : this()
    {
        SetProjectService(projectService);
        SetOperationInfoService(operationInfoService);
    }

    #region Set data
    public void SetProjectService(ProjectService value)
    {
        ProjectService = Guard.Against.Null(value, nameof(value));
        ProjectServiceId = Guard.Against.Null(value.Id, nameof(value.Id));
    }
    public void SetOperationInfoService(OperationInfoService value)
    {
        OperationInfoService = Guard.Against.Null(value, nameof(value));
        OperationInfoServiceId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    #endregion

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private List<ProjectOperationDetailContractorService> _contractorServices;
    public IReadOnlyList<ProjectOperationDetailContractorService> ProjectOperationDetailContractorServices => _contractorServices;
    private ProjectServiceDetail()
    {
        _contractorServices = new List<ProjectOperationDetailContractorService>();
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}
