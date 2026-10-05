namespace Engineering.Domain.Entities.Projects.ProjectUsers;

[Description(ProjectCmts.ProjectImplementationAssistant)]
public class ProjectImplementationAssistant : AuditableEntity<ProjectImplementationAssistant>
{
    [Description(ProjectCmts.ImplementationAssistantUserId)]
    public long ImplementationAssistantUserId { get; set; }

    [Description(GlobalCmts.Project)]
    public long ProjectId { get; set; }
    public Project Project { get; set; }

    public ProjectImplementationAssistant(Project project, long implementationAssistantUserId)
    {
        Project = Guard.Against.Null(project, nameof(project));
        ImplementationAssistantUserId = Guard.Against.Null(implementationAssistantUserId, nameof(implementationAssistantUserId));
    }

    public void SetImplementationAssistantUserId(long value)
    {
        ImplementationAssistantUserId = Guard.Against.Null(value, nameof(value));
    }

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private ProjectImplementationAssistant() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
