namespace Engineering.Domain.Entities.Projects.Junctions;

[Description(ProjectCmts.ProjectProduct)]
public class ProjectCategory : AuditableEntity<ProjectCategory, long>
{
    [Description(ProjectCmts.Project)]
    public Project Project { get; private set; }
    [Description(ProjectCmts.ProjectId)]
    public long ProjectId { get; private set; }

    [Description(GlobalCmts.Category)]
    public Category Category { get; private set; }
    [Description(GlobalCmts.CategoryId)]
    public long CategoryId { get; private set; }

    public ProjectCategory(
       Project project,
       Category category) : this()
    {
        SetProject(project);
        SetCategory(category);
    }

    public void SetProject(Project value)
    {
        Project = Guard.Against.Null(value, nameof(value));
        ProjectId = Guard.Against.Null(value.Id, nameof(value));
    }

    public void SetCategory(Category value)
    {
        Category = Guard.Against.Null(value, nameof(value));
        CategoryId = Guard.Against.Null(value.Id, nameof(value));
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private ProjectCategory() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}