
namespace Engineering.Domain.Entities.ProjectOperationDetails;

/// <summary>
/// مستندات پیوست ریزمتره
/// </summary>
public class ProjectOperationDetailDocument : AuditableEntity<ProjectOperationDetailDocument>
{
    [Description(GlobalCmts.Url)]
    public string Url { get; private set; } = string.Empty;

    [Description(ProjectOperationCmts.ProjectOperationDetail)]
    public ProjectOperationDetail ProjectOperationDetail { get; private set; }

    public ProjectOperationDetailDocument(string url,
        ProjectOperationDetail projectOperationDetail) : this()
    {
        Url = Guard.Against.NullOrEmpty(url, nameof(url));
        ProjectOperationDetail = Guard.Against.Null(projectOperationDetail, nameof(projectOperationDetail));
    }

    public static ProjectOperationDetailDocument Create(string url, ProjectOperationDetail projectOperationDetail)
    {
        return new ProjectOperationDetailDocument(url, projectOperationDetail);
    }

    public void SetIsDeleted()
    {
        this.IsDeleted = true;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private ProjectOperationDetailDocument() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}
