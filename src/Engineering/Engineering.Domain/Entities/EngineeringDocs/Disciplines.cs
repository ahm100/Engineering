namespace Engineering.Domain.Entities.EngineeringDocs;


public class Discipline : ActivateEntity<Discipline>
{
    [Description(GlobalCmts.Code)]
    public string Code { get; private set; } = string.Empty;

    [Description(DisciplineCmts.Name)]
    public string Name { get; private set; } = string.Empty;

    [Description(DisciplineCmts.EnglishName)]
    public string EnglishName { get; private set; } = string.Empty;

    [Description(GlobalCmts.Description)]
    public string Description { get; private set; } = string.Empty;

    private readonly List<DisciplineDocType> _disciplineDocTypes = [];

    [Description(DisciplineCmts.DisciplineDocTypes)]
    public IReadOnlyList<DisciplineDocType> DisciplineDocTypes => _disciplineDocTypes;

    private readonly List<ProjectDoc> _projectDocs = [];

    [Description(DisciplineCmts.ProjectDoc)]
    public IReadOnlyList<ProjectDoc> ProjectDocs => _projectDocs;

    public Discipline(
        string code,
        string name,
        string englishName,
        string description)
    {
        Code = code;
        Name = name;
        EnglishName = englishName;
        Description = description;
        IsActive = true;
    }

    public void Update(
        string code,
        string name,
        string englishName,
        string description)
    {
        Code = code;
        Name = name;
        EnglishName = englishName;
        Description = description;
    }

    /// <summary>
    /// For EF Core, never touch this.
    /// </summary>
    private Discipline()
    {
    }
}