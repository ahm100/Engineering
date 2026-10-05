
namespace Engineering.Application.Services.EngineeringDocs.Contracts.GetProjectDocs;

public record GetProjectDocsRequest(
    long? ProjectId,
    long? DisciplineId,
    long? DisciplineDocId,
    int PageIndex,
    int PageSize
) : IHttpRequest;