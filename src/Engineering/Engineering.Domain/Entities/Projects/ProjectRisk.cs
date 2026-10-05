using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Domain.Entities.Projects;

[Description(ProjectCmts.ProjectRisk)]
public class ProjectRisk : ActivateEntity<ProjectRisk, long>
{
    [Description(GlobalCmts.Code)]
    public string Code { get; private set; } = string.Empty;

    [Description(GlobalCmts.Title)]
    public string Title { get; private set; } = string.Empty;

    [Description(GlobalCmts.Project)]
    public long ProjectId { get; private set; }
    public Project Project { get; private set; }

    [Description(ProjectCmts.RiskProbability)]
    public RiskProbability RiskProbability { get; private set; }

    [Description(ProjectCmts.RiskImpact)]
    public RiskImpact RiskImpact { get; private set; }

    [Description(GlobalCmts.Status)]
    public RiskStatus RiskStatus { get; private set; }

    public ProjectRisk(Project project,
       string code,
       string title,
       RiskProbability riskProbability,
       RiskImpact riskImpact,
       RiskStatus riskStatus,
       bool isActive) : this()
    {
        SetProject(project);
        SetCode(code);
        SetTitle(title);
        SetRiskProbability(riskProbability);
        SetRiskImpact(riskImpact);
        SetRiskStatus(riskStatus);
        if (isActive)
            SetActive();
        else
            SetDeactivate();
    }

    public void Update(Project? project,
       string? code,
       string? title,
       RiskProbability? riskProbability,
       RiskImpact? riskImpact,
       RiskStatus? riskStatus,
       bool isActive)
    {
        SetProject(project ?? Project);
        SetCode(code ?? Code);
        SetTitle(title ?? Title);
        SetRiskProbability(riskProbability ?? RiskProbability);
        SetRiskImpact(riskImpact ?? RiskImpact);
        SetRiskStatus(riskStatus ?? RiskStatus);
        if (isActive)
            SetActive();
        else
            SetDeactivate();
    }


    public void SetProject(Project value)
    {
        Project = Guard.Against.Null(value, nameof(value));
        ProjectId = Guard.Against.Null(value.Id, nameof(value));
    }

    public void SetCode(string value)
    {
        Code = Guard.Against.NullOrEmpty(value, nameof(value));
    }

    public void SetTitle(string value)
    {
        Title = Guard.Against.NullOrEmpty(value, nameof(value));
    }

    public void SetRiskProbability(RiskProbability value)
    {
        RiskProbability = Guard.Against.Null(value, nameof(value));
    }

    public void SetRiskImpact(RiskImpact value)
    {
        RiskImpact = Guard.Against.Null(value, nameof(value));
    }

    public void SetRiskStatus(RiskStatus value)
    {
        RiskStatus = Guard.Against.Null(value, nameof(value));
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private ProjectRisk() { }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
