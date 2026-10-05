
namespace Engineering.Application.Services.EngineeringDocs.Contracts.GetDisciplineDocType;

public record GetDisciplineDocTypeRequest(
    long? DisciplineId,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;
