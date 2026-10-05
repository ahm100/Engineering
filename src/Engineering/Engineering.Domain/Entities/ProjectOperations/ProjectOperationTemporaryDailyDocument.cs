
namespace Engineering.Domain.Entities.ProjectOperations;

[Description(GlobalCmts.Document)]
public class ProjectOperationTemporaryDailyDocument : AuditableEntity<ProjectOperationTemporaryDailyDocument>
{
    [Description(GlobalCmts.Url)]
    public string Url { get; private set; } = string.Empty;

    [Description(ProjectOperationCmts.ProjectOperationTemporaryDaily)]
    public long ProjectOperationTemporaryDailyId { get; private set; }
    public ProjectOperationTemporaryDaily ProjectOperationTemporaryDaily { get; private set; }

    public ProjectOperationTemporaryDailyDocument(string url, ProjectOperationTemporaryDaily projectOperationTemporaryDaily) : this()
    {
        SetUrl(url);
        SetProjectOperationTemporaryDaily(projectOperationTemporaryDaily);
    }

    public static ProjectOperationTemporaryDailyDocument Create(string url, ProjectOperationTemporaryDaily projectOperationTemporaryDaily)
    {
        return new ProjectOperationTemporaryDailyDocument(url, projectOperationTemporaryDaily);
    }

    public void SetUrl(string value)
    {
        Url = Guard.Against.NullOrEmpty(value, nameof(value)); ;
    }
    public void SetProjectOperationTemporaryDaily(ProjectOperationTemporaryDaily value)
    {
        ProjectOperationTemporaryDaily = Guard.Against.Null(value, nameof(value));
        ProjectOperationTemporaryDailyId = Guard.Against.Null(value.Id, nameof(value.Id));
    }
    public void SetIsDeleted()
    {
        this.IsDeleted = true;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private ProjectOperationTemporaryDailyDocument() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}
