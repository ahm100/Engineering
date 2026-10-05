using Engineering.Domain.Entities.ProjectOperations.Enums;
using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Domain.Entities.Projects.Histories;

[Description(ProjectCmts.ProjectHistory)]
public class ProjectHistory : ActivateEntity<ProjectHistory, long>
{
    [Description(ProjectCmts.ProjectName)]
    public string ProjectName { get; private set; } = string.Empty;
    [Description(ProjectCmts.ProjectName)]
    public string? ProjectEnName { get; private set; } = string.Empty;

    [Description(ProjectCmts.ProjectCode)]
    public string? ProjectCode { get; private set; } = string.Empty;
    [Description(ProjectCmts.Prefix)]
    public string? Prefix { get; private set; }
    [Description(ProjectCmts.EmployerId)]
    public long? EmployerId { get; private set; }
    [Description(ProjectCmts.SupervisorEngineer)]

    public long? SupervisorEngineer { get; private set; } = default;
    [Description(ProjectCmts.Advisor)]
    public long? Advisor { get; private set; } = default;
    [Description(ProjectCmts.ProjectManager)]
    public long? ProjectManager { get; private set; } = default;
    [Description(ProjectCmts.PlanningAssistant)]
    public long? PlanningAssistant { get; private set; } = default;
    [Description(ProjectCmts.Status)]
    public ProjectStatus Status { get; private set; }
    [Description(ProjectCmts.AddAutomated)]
    public bool AddAutomated { get; private set; } = false;
    [Description(ProjectCmts.Contractual)]
    public bool Contractual { get; private set; } = true;
    [Description(ProjectCmts.CollectiveService)]
    public bool CollectiveService { get; private set; } = false;
    [Description(GlobalCmts.CompanyId)]
    public long? CompanyId { get; private set; }

    [Description(GlobalCmts.CityId)]
    public long? CityId { get; private set; }

    [Description(GlobalCmts.Description)]
    public string? Description { get; private set; }

    [Description(GlobalCmts.Description)]
    public string? DescriptionEn { get; private set; }

    [Description(ProjectCmts.AddressDescription)]
    public string? AddressDescription { get; private set; }

    [Description(GlobalCmts.PreferentialReferenceCode)]
    public Guid PreferentialReferenceCode { get; private set; }

    [Description(ProjectCmts.ApprovedBudget)]
    public decimal? ApprovedBudget { get; private set; }

    [Description(GlobalCmts.Project)]
    public Project Project { get; set; }
    public long ProjectId { get; set; }

    [Description(ProjectCmts.IsOrganizationUnit)]
    public bool IsOrganizationUnit { get; private set; } = false;

    [Description(ProjectCmts.OrganizationId)]
    public long? OrganizationId { get; private set; }

    public ProjectHistory(
        Project project) : this()
    {
        SetProject(project);
        SetProjectName(project.ProjectName);
        SetEnName(project.ProjectEnName);
        SetProjectCode(project.ProjectCode);
        SetPrefix(project.Prefix);
        SetEmployerId(project.EmployerId);
        SetSupervisorEngineer(project.SupervisorEngineer);
        SetAdvisor(project.Advisor);
        SetProjectManager(project.ProjectManager);
        SetPlanningAssistant(project.PlanningAssistant);
        SetStatus(project.Status);
        SetCompanyId(project.CompanyId);
        SetContractual(project.Contractual);
        SetCollectiveService(project.CollectiveService);
        SetApprovedBudget(project.ApprovedBudget);
        IsActive = project.IsActive;
        SetPreferentialReferenceCode(Guid.NewGuid());
        SetDescription(project.Description);
        SetDescriptionEn(project.DescriptionEn);
        SetAddressDescription(project.AddressDescription);
        SetIsOrganizationUnit(project.IsOrganizationUnit);
        SetOrganizationId(project.OrganizationId);
    }

    #region Set data

    public void SetProjectName(string value)
    {
        ProjectName = Guard.Against.Null(value, nameof(value));
    }

    public void SetPreferentialReferenceCode(Guid value)
    {
        PreferentialReferenceCode = Guard.Against.Null(value, nameof(value));
    }

    public void SetProjectCode(string? value)
    {
        ProjectCode = value;
    }
    public void SetPrefix(string? value)
    {
        Prefix = value;
    }
    public void SetDescriptionEn(string? value)
    {
        DescriptionEn = value;
    }
    public void SetEnName(string? value)
    {
        ProjectEnName = value;
    }


    public void SetCityId(long? value)
    {
        CityId = value;
    }

    public void SetDescription(string? value)
    {
        Description = value;
    }

    public void SetAddressDescription(string? value)
    {
        AddressDescription = value;
    }

    public void SetApprovedBudget(decimal? value)
    {
        ApprovedBudget = value;
    }

    public void SetStatus(ProjectStatus value)
    {
        Status = Guard.Against.Null(value, nameof(value));
    }

    public ProjectStatus StatusChecker(Project entity)
    {
        var status = ProjectStatus.NotStarted;

        if (entity.ProjectOperations.All(x => x.ProjectOperationStatus == ProjectOperationStatus.NotStarted))
            status = ProjectStatus.NotStarted;
        else if (entity.ProjectOperations.All(x => x.ProjectOperationStatus == ProjectOperationStatus.Doing))
            status = ProjectStatus.Doing;
        else if (entity.ProjectOperations.All(x => x.ProjectOperationStatus == ProjectOperationStatus.Stopped))
            status = ProjectStatus.Stopped;
        else if (entity.ProjectOperations.All(x => x.ProjectOperationStatus == ProjectOperationStatus.EndOfWork))
            status = ProjectStatus.EndOfWork;
        else if (entity.ProjectOperations.All(x => x.ProjectOperationStatus == ProjectOperationStatus.TemporaryDelivery))
            status = ProjectStatus.TemporaryDelivery;
        else if (entity.ProjectOperations.All(x => x.ProjectOperationStatus == ProjectOperationStatus.DefiniteDelivery))
            status = ProjectStatus.DefiniteDelivery;

        else if (entity.ProjectOperations.Any(x => x.ProjectOperationStatus == ProjectOperationStatus.Doing))
            status = ProjectStatus.Doing;

        else
            status = ProjectStatus.Doing;

        return status;
    }
    public void SetName(string value)
    {
        ProjectName = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }
    public void SetCollectiveService(bool value)
    {
        CollectiveService = value;
    }
    public void SetCode(string value)
    {
        ProjectCode = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }
    public void SetEmployerId(long? value)
    {
        EmployerId = value;
    }
    public void SetProject(Project value)
    {
        Project = Guard.Against.Null(value, nameof(value));
        ProjectId = Guard.Against.Null(value.Id, nameof(value.Id));
    }
    public void SetAdvisor(long? value)
    {
        Advisor = value;
    }
    public void SetContractual(bool value)
    {
        Contractual = value;
    }
    public void SetSupervisorEngineer(long? value)
    {
        SupervisorEngineer = value;
    }
    public void SetProjectManager(long? value)
    {
        ProjectManager = value;
    }
    public void SetPlanningAssistant(long? value)
    {
        PlanningAssistant = value;
    }
    public void SetInActive()
    {
        IsActive = false;
    }
    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    public void SetIsOrganizationUnit(bool value)
    {
        IsOrganizationUnit = Guard.Against.Null(value, nameof(value));
    }

    public void SetOrganizationId(long? value)
    {
        OrganizationId = value;
    }

    public void SetCompanyId(long? value)
    {
        CompanyId = value;
    }
    #endregion

    #region Methods 

    #endregion

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.


    private ProjectHistory()
    {
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}
