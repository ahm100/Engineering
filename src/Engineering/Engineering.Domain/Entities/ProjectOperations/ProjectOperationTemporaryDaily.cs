using Engineering.Domain.Entities.CostCenters;
using Engineering.Domain.Entities.ProjectOperations.Enums;
using Engineering.Domain.Entities.Projects;

namespace Engineering.Domain.Entities.ProjectOperations;

[Description(ProjectOperationCmts.ProjectOperationTemporaryDaily)]
public class ProjectOperationTemporaryDaily : AuditableEntity<ProjectOperationTemporaryDaily>
{
    [Description(GlobalCmts.StartDate)]
    public DateTime StartDate { get; private set; }
    [Description(GlobalCmts.EndDate)]
    public DateTime EndDate { get; private set; }
    [Description(GlobalCmts.Description)]
    public string? Description { get; private set; }
    [Description(GlobalCmts.Status)]
    public TemporaryDailyStatus? Status { get; private set; } = TemporaryDailyStatus.NotStarted;

    [Description(GlobalCmts.CostCenter)]
    public long CostCenterId { get; set; }
    public CostCenter CostCenter { get; set; }

    [Description(GlobalCmts.Project)]
    public long ProjectId { get; set; }
    public Project Project { get; set; }

    [Description(GlobalCmts.ProjectOperation)]
    public long? ProjectOperationId { get; set; }
    public ProjectOperation? ProjectOperation { get; set; }


    public ProjectOperationTemporaryDaily(CostCenter costCenter,
        Project project,
        ProjectOperation? projectOperation,
        DateTime startDate,
        DateTime endDate,
        string? description,
        TemporaryDailyStatus? status) : this()
    {
        SetCostCenter(costCenter);
        SetProject(project);
        SetProjectOperation(projectOperation);
        SetStartDate(startDate);
        SetEndDate(endDate);
        ChangeStatus(status);
        SetDescription(description);

    }

    public static ProjectOperationTemporaryDaily Create(CostCenter costCenter,
        Project project,
        ProjectOperation? projectOperation,
        DateTime startDate,
        DateTime endDate,
        string? description,
        TemporaryDailyStatus? status)
    {
        return new ProjectOperationTemporaryDaily(
            costCenter, project, projectOperation, startDate, endDate, description, status);
    }
    #region Set data

    public void SetCostCenter(CostCenter value)
    {
        CostCenter = Guard.Against.Null(value, nameof(value));
        CostCenterId = Guard.Against.Null(value.Id, nameof(value.Id));
    }
    public void SetProject(Project value)
    {
        Project = Guard.Against.Null(value, nameof(value));
        ProjectId = Guard.Against.Null(value.Id, nameof(value.Id));
    }
    public void SetProjectOperation(ProjectOperation? value)
    {
        ProjectOperation = value;
        ProjectOperationId = value?.Id;
    }
    public void SetStartDate(DateTime value)
    {
        StartDate = Guard.Against.Null(value, nameof(value));
    }
    public void SetEndDate(DateTime value)
    {
        EndDate = Guard.Against.Null(value, nameof(value));
    }
    public void ChangeStatus(TemporaryDailyStatus? value)
    {
        Status = value;
    }
    public void SetDescription(string? value)
    {
        Description = value;
    }

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    #endregion

    public void AddDocuments(List<string>? urls)
    {
        if (urls != null && urls.Count > 0)
        {
            if (_projectOperationTemporaryDailyDocuments.Any())
                _projectOperationTemporaryDailyDocuments.ForEach(c => c.SetIsDeleted());

            foreach (var url in urls)
                _projectOperationTemporaryDailyDocuments.Add(ProjectOperationTemporaryDailyDocument.Create(url, this));
        }
        else
        {
            if (_projectOperationTemporaryDailyDocuments.Any())
                _projectOperationTemporaryDailyDocuments.ForEach(c => c.SetIsDeleted());
        }
    }

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private List<ProjectOperationTemporaryDailyDocument> _projectOperationTemporaryDailyDocuments;
    public IReadOnlyList<ProjectOperationTemporaryDailyDocument> ProjectOperationTemporaryDailyDocuments => _projectOperationTemporaryDailyDocuments;

    private ProjectOperationTemporaryDaily()
    {
        _projectOperationTemporaryDailyDocuments = [];
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}