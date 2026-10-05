using Engineering.Domain.Entities.EngineeringDocs.Enums;
using System.ComponentModel;

namespace Engineering.Application.Services.EngineeringDocs.Contracts.GetProjectDocs;

public class GetProjectDocsModel
{
    public long Id { get; set; }
    public long ProjectId { get; set; }
    public long DisciplineId { get; set; }
    public long DisciplineDocId { get; set; }
    [Description(DisciplineCmts.DisciplineDocTitle)]
    public string DisciplineDocTitle { get; set; } = string.Empty;
    [Description(DisciplineCmts.DisciplineDocEnTitle)]
    public string DisciplineDocEnTitle { get; set; } = string.Empty;
    public long ThirdPartyId { get; set; }

    public string Url { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;

    public int Revision { get; set; }
    public int Sequence { get; set; }
    public ProjectDocStatusEnum Status { get; set; }

    public long CreatorId { get; set; }
    public DateTime Created { get; set; }
    public long? UpdaterId { get; set; }
    public DateTime? Updated { get; set; }
}