
namespace Engineering.Domain.Entities.EngineeringDocs;


public class DisciplineDoc : ActivateEntity<DisciplineDoc>
{
    [Description(GlobalCmts.Code)]
    public string Code { get; private set; } = string.Empty;

    [Description(GlobalCmts.Title)]
    public string Title { get; private set; } = string.Empty;

    [Description(DisciplineCmts.DisciplineDocEnTitle)]
    public string EnTitle { get; set; }

    [Description(GlobalCmts.Description)]
    public string Description { get; private set; } = string.Empty;

    private readonly List<DisciplineDocType> _disciplineDocTypes = [];

    [Description(DisciplineCmts.DisciplineDocTypes)]
    public IReadOnlyList<DisciplineDocType> DisciplineDocTypes => _disciplineDocTypes;

    private readonly List<ProjectDoc> _projectDocs = [];

    [Description(DisciplineCmts.ProjectDoc)]
    public IReadOnlyList<ProjectDoc> ProjectDocs => _projectDocs;

    public DisciplineDoc(
        string code,
        string title,
        string? enTitle,
        string description)
    {
        Code = code;
        Title = title;
        EnTitle = enTitle ?? "";
        Description = description;
        IsActive = true;

    }

    public void Update(
        string code,
        string title,
        string? enTitle,
        string description)
    {
        Code = code;
        Title = title;
        EnTitle = enTitle ?? "";
        Description = description;
    }

    /// <summary>
    /// For EF Core, never touch this.
    /// </summary>
    private DisciplineDoc()
    {
    }
}