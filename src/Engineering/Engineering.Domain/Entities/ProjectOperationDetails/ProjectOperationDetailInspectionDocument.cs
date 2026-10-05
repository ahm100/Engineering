
namespace Engineering.Domain.Entities.ProjectOperationDetails;

[Description(ProjectDetailCmts.ProjectOperationDetailInspection)]
public class ProjectOperationDetailInspectionDocument : AuditableEntity<ProjectOperationDetailInspectionDocument>
{
    [Description(GlobalCmts.Url)]
    public string Url { get; private set; } = string.Empty;

    [Description(ProjectDetailCmts.ProjectOperationDetailInspection)]
    public long ProjectOperationDetailInspectionId { get; private set; }
    public ProjectOperationDetailInspection ProjectOperationDetailInspection { get; private set; }

    public ProjectOperationDetailInspectionDocument(string url,
        ProjectOperationDetailInspection projectOperationDetailInspection) : this()
    {
        Url = Guard.Against.NullOrEmpty(url, nameof(url));
        ProjectOperationDetailInspection = Guard.Against.Null(projectOperationDetailInspection, nameof(projectOperationDetailInspection));
    }

    public static ProjectOperationDetailInspectionDocument Create(string url,
        ProjectOperationDetailInspection projectOperationDetailInspection)
    {
        return new ProjectOperationDetailInspectionDocument(url,
            projectOperationDetailInspection);
    }

    public void SetUrl(string value)
    {
        Url = Guard.Against.NullOrEmpty(value, nameof(value)); ;
    }
    public void SetProjectOperationDetailInspection(ProjectOperationDetailInspection projectOperationDetailInspection)
    {
        ProjectOperationDetailInspection = Guard.Against.Null(projectOperationDetailInspection, nameof(projectOperationDetailInspection));
        ProjectOperationDetailInspectionId = Guard.Against.Null(projectOperationDetailInspection.Id, nameof(projectOperationDetailInspection.Id));
    }
    public void SetIsDeleted()
    {
        this.IsDeleted = true;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private ProjectOperationDetailInspectionDocument() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}
