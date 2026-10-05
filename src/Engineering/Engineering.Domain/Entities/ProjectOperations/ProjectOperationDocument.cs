
namespace Engineering.Domain.Entities.ProjectOperations;

[Description(ProjectOperationCmts.ProjectOperationDocument)]
public class ProjectOperationDocument : AuditableEntity<ProjectOperationDocument>
{
    [Description(GlobalCmts.Url)]
    public string Url { get; private set; } = string.Empty;

    [Description(GlobalCmts.ProjectOperation)]
    public long ProjectOperationId { get; private set; }
    public ProjectOperation ProjectOperation { get; private set; }

    public ProjectOperationDocument(string url, ProjectOperation projectOperation)
    {
        Url = Guard.Against.NullOrEmpty(url, nameof(url));
        ProjectOperation = Guard.Against.Null(projectOperation, nameof(projectOperation));
    }

    public static ProjectOperationDocument Create(string url, ProjectOperation ProjectOperation)
    {
        return new ProjectOperationDocument(url, ProjectOperation);
    }

    public void SetIsDeleted()
    {
        this.IsDeleted = true;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private ProjectOperationDocument() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}
