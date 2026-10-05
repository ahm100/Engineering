

namespace Engineering.Domain.Entities.EngineeringDocs;

public class DisciplineDocType : ActivateEntity<DisciplineDocType>
{
    [Description(DisciplineCmts.DisciplineId)]
    public long DisciplineId { get; private set; }

    [Description(DisciplineCmts.DisciplineDocId)]
    public long DisciplineDocId { get; private set; }

    [Description(GlobalCmts.Code)]
    public string Code { get; private set; } = string.Empty;

    [Description(GlobalCmts.Description)]
    public string Description { get; private set; } = string.Empty;

    [Description(DisciplineCmts.Discipline)]
    public Discipline Discipline { get; private set; } = null!;

    [Description(DisciplineCmts.DisciplineDoc)]
    public DisciplineDoc DisciplineDoc { get; private set; } = null!;

    public DisciplineDocType(
        long disciplineId,
        long disciplineDocId,
        string code,
        string description)
    {
        DisciplineId = disciplineId;
        DisciplineDocId = disciplineDocId;
        Code = code;
        Description = description;
        IsActive = true;
    }

    public void Update(
        long disciplineId,
        long disciplineDocId,
        string code,
        string description)
    {
        DisciplineId = disciplineId;
        DisciplineDocId = disciplineDocId;
        Code = code;
        Description = description;
    }

    /// <summary>
    /// For EF Core, never touch this.
    /// </summary>
    private DisciplineDocType()
    {
    }
}