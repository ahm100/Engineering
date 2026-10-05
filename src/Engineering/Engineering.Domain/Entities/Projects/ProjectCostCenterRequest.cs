using Engineering.Domain.Entities.CostCenters;
using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Domain.Entities.Projects.ProjectCostCenterRequests;

[Description(GlobalCmts.ProjectCostCenterRequest)]
public class ProjectCostCenterRequest : AuditableEntity<ProjectCostCenterRequest>
{
    [Description(GlobalCmts.ProjectId)]
    public long ProjectId { get; private set; }

    [Description(GlobalCmts.Project)]
    public Project Project { get; private set; } = null!;

    [Description(GlobalCmts.CostCenterId)]
    public long? CostCenterId { get; private set; }

    [Description(GlobalCmts.CostCenter)]
    public CostCenter? CostCenter { get; private set; }

    [Description(GlobalCmts.RejectionReason)]
    public string? RejectionReason { get; private set; }

    [Description(GlobalCmts.RequestedCostCenterName)]
    public string? RequestedCostCenterName { get; private set; }

    [Description(GlobalCmts.Description)]
    public string Description { get; private set; } = string.Empty;

    [Description(GlobalCmts.Status)]
    public ProjectCostCenterRequestStatus Status { get; private set; } = ProjectCostCenterRequestStatus.InProgress;

    private ProjectCostCenterRequest() { } // For EF Core

    public ProjectCostCenterRequest(
        long projectId,
        string? suggestedName,
        string description,
        long thirdPartyId,
        long userId)
    {
        ProjectId = projectId;
        SetSuggestedName(suggestedName);
        SetDescription(description);
    }

    public void SetSuggestedName(string? value)
        => RequestedCostCenterName = string.IsNullOrWhiteSpace(value) ? null : value.Trim();

    public void SetDescription(string value)
        => Description = Guard.Against.NullOrWhiteSpace(value, nameof(value));

    public bool CanEdit() => Status == ProjectCostCenterRequestStatus.InProgress;

    public void Reject(string reason)
    {
        RejectionReason = Guard.Against.NullOrWhiteSpace(reason, nameof(reason));
        Status = ProjectCostCenterRequestStatus.Rejected;
    }

    public void Complete(CostCenter costCenter)
    {
        Status = ProjectCostCenterRequestStatus.Completed;
        CostCenter = costCenter;
    }
}
