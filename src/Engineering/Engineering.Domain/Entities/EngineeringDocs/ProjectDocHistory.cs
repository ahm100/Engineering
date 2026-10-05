using Engineering.Domain.Entities.EngineeringDocs.Enums;
using Engineering.Domain.Entities.Projects;

namespace Engineering.Domain.Entities.EngineeringDocs;

public class ProjectDocHistory : AuditableEntity<ProjectDocHistory>
{
    [Description(GlobalCmts.ProjectId)]
    public long ProjectId { get; private set; }

    [Description(DisciplineCmts.DisciplineId)]
    public long DisciplineId { get; private set; }

    [Description(DisciplineCmts.DisciplineDocId)]
    public long DisciplineDocId { get; private set; }

    [Description(GlobalCmts.Project)]
    public Project Project { get; private set; } = null!;

    [Description(DisciplineCmts.Discipline)]
    public Discipline Discipline { get; private set; } = null!;

    [Description(DisciplineCmts.DisciplineDoc)]
    public DisciplineDoc DisciplineDoc { get; private set; } = null!;

    [Description(GlobalCmts.Url)]
    public string Url { get; private set; } = string.Empty;

    [Description(GlobalCmts.Description)]
    public string Description { get; private set; } = string.Empty;

    [Description(ProjectCmts.ThirdPartyId)]
    public long ThirdPartyId { get; private set; }

    [Description(GlobalCmts.Code)]
    public string Code { get; private set; } = string.Empty;

    [Description(ProjectCmts.Revision)]
    public int Revision { get; private set; }

    [Description(ProjectCmts.Sequence)]
    public int Sequence { get; private set; }

    [Description(ProjectCmts.Status)]
    public ProjectDocStatusEnum Status { get; private set; }
    public long ProjectDocId { get; private set; }

    public ProjectDoc ProjectDoc { get; private set; } = null!;

    public ProjectDocHistory(ProjectDoc projectDoc) : this()
    {
        ProjectDocId = projectDoc.Id;
        ProjectId = projectDoc.ProjectId;
        DisciplineId = projectDoc.DisciplineId;
        DisciplineDocId = projectDoc.DisciplineDocId;
        ThirdPartyId = projectDoc.ThirdPartyId;
        Url = projectDoc.Url;
        Description = projectDoc.Description;
        Code = projectDoc.Code;
        Revision = projectDoc.Revision;
        Sequence = projectDoc.Sequence;
        Status = projectDoc.Status;
    }

#pragma warning disable CS8618
    private ProjectDocHistory()
    {
    }
#pragma warning restore CS8618
}