using Engineering.Domain.Entities.EngineeringDocs.Enums;
using System.ComponentModel;


namespace Engineering.Application.Services.EngineeringDocs.Contracts.GetProjectDocById;


public record GetProjectDocByIdResponse
{
    public long Id { get; init; }
    public long ProjectId { get; init; }
    public long DisciplineId { get; init; }
    public long DisciplineDocId { get; init; }
    [Description(DisciplineCmts.DisciplineDocTitle)]
    public string DisciplineDocTitle { get; set; } = string.Empty;
    [Description(DisciplineCmts.DisciplineDocEnTitle)]
    public string DisciplineDocEnTitle { get; set; } = string.Empty;
    public long ThirdPartyId { get; init; }
    public string Url { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string Code { get; init; } = string.Empty;
    public int Revision { get; init; }
    public int Sequence { get; init; }
    public ProjectDocStatusEnum Status { get; init; }
}