using Engineering.Domain.Entities.CostCenters;

namespace Engineering.Domain.Entities.Projects.Junctions;

public class ProjectCostCenter : AuditableEntity<ProjectCostCenter>
{
    [Description(GlobalCmts.Project)]
    public long ProjectId { get; private set; }
    public Project Project { get; private set; }

    [Description(GlobalCmts.CostCenter)]
    public long CostCenterId { get; private set; }
    public CostCenter CostCenter { get; private set; }

    public bool IsDefault { get; private set; } = false;

    public ProjectCostCenter(
        Project project,
        CostCenter costCenter,
        bool isDefault) : this()
    {
        SetProject(project);
        SetCostCenter(costCenter);
        SetIsDefault(isDefault);
    }

    public void SetProject(Project value)
    {
        Project = Guard.Against.Null(value, nameof(value));
        ProjectId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetCostCenter(CostCenter value)
    {
        CostCenter = Guard.Against.Null(value, nameof(value));
        CostCenterId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetIsDefault(bool value)
    {
        IsDefault = Guard.Against.Null(value, nameof(value));
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private ProjectCostCenter() { }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
