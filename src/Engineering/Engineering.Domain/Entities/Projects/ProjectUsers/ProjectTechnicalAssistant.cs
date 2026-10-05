namespace Engineering.Domain.Entities.Projects.ProjectUsers;

/// <summary>
/// مسئولین فنی 
/// </summary>
public class ProjectTechnicalAssistant : AuditableEntity<ProjectTechnicalAssistant>
{
    [Description(ProjectCmts.TechnicalAssistantUserId)]
    public long TechnicalAssistantUserId { get; private set; }

    [Description(GlobalCmts.Project)]
    public long ProjectId { get; private set; }
    public Project Project { get; private set; }

    public ProjectTechnicalAssistant(Project project,
        long technicalAssistantUserId) : this()
    {
        SetProject(project);
        SetTechnicalAssistantUserId(technicalAssistantUserId);
    }

    public void SetTechnicalAssistantUserId(long value)
    {
        TechnicalAssistantUserId = Guard.Against.Null(value, nameof(value));
    }

    public void SetProject(Project project)
    {
        Project = Guard.Against.Null(project, nameof(project));
        ProjectId = Guard.Against.Null(project.Id, nameof(project.Id));
    }

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private ProjectTechnicalAssistant() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
