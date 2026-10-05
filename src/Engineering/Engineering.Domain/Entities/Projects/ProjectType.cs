
using Engineering.Domain.Entities.Projects.Histories;

namespace Engineering.Domain.Entities.Projects;

[Description(ProjectCmts.ProjectType)]
public class ProjectType : ActivateEntity<ProjectType, long>
{
    [Description(ProjectCmts.ProjectTypeCode)]
    public string ProjectTypeCode { get; private set; } = string.Empty;
    [Description(ProjectCmts.ProjectTypeTitle)]
    public string ProjectTypeTitle { get; private set; } = string.Empty;
    [Description(GlobalCmts.CompanyId)]
    public long? CompanyId { get; private set; }

    public ProjectType(
        string title,
        string code,
        bool isActive,
        long? companyId) : this()
    {
        SetCode(code);
        SetName(title);
        if (isActive)
            SetActive();
        else
            SetDeactivate();
        SetCompanyId(companyId);
    }

    #region Set data

    public void SetName(string value)
    {
        ProjectTypeTitle = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }
    public void SetCode(string value)
    {
        ProjectTypeCode = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }
    public void SetCompanyId(long? value)
    {
        CompanyId = value;
    }

    #endregion

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    [Description(GlobalCmts.Project)]
    private List<Project> _projects;
    public IReadOnlyList<Project> Projects => _projects;
    [Description(ProjectCmts.ProjectHistory)]
    private readonly List<ProjectHistory> _projectHistories;
    public IReadOnlyList<ProjectHistory> ProjectHistories => _projectHistories;
    private ProjectType()
    {
        _projects = [];
        _projectHistories = [];
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}
