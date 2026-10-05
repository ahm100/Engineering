using Engineering.Domain.Entities.EngineeringDocs.Enums;
using Engineering.Domain.Entities.Projects;

namespace Engineering.Domain.Entities.EngineeringDocs;

public class ProjectDoc : ActivateEntity<ProjectDoc>
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

    public ProjectDoc(
    long projectId,
    long disciplineId,
    long disciplineDocId,
    long thirdPartyId,
    string url)
    {
        ProjectId = projectId;
        DisciplineId = disciplineId;
        DisciplineDocId = disciplineDocId;
        ThirdPartyId = thirdPartyId;
        Url = url;
        Status = ProjectDocStatusEnum.Draft;
    }

    public void Update(
    long disciplineId,
    long disciplineDocId,
    long thirdPartyId,
    string url)
    {
        DisciplineId = disciplineId;
        DisciplineDocId = disciplineDocId;
        ThirdPartyId = thirdPartyId;
        Url = url;
    }

    public void SetSequence(int sequence)
    {
        Sequence = sequence;
    }


    public void SetCode(
        string projectCode,
        string disciplineCode,
        string disciplineDocCode,
        int sequence)
    {
        Code = ProjectDocCodeGenerator.Generate(
            projectCode,
            disciplineCode,
            disciplineDocCode,
            sequence,
            Revision);
    }

    public void SetRevision(int revision)
    {
        Revision = revision;
    }



    private static class ProjectDocCodeGenerator
    {
        public static string Generate(
            string projectCode,
            string disciplineCode,
            string disciplineDocCode,
            int sequence,
            int revision)
        {
            return $"{projectCode}-{disciplineCode}-{disciplineDocCode}-{sequence:D3}-R{revision:D3}";
        }
    }

    /// <summary>
    /// For EF Core, never touch this.
    /// </summary>
    private ProjectDoc()
    {
    }
}