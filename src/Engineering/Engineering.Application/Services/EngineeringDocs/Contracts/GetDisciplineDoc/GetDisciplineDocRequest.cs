
namespace Engineering.Application.Services.EngineeringDocs.Contracts.GetDisciplineDoc;

public record GetDisciplineDocRequest(
    int PageIndex,
    int PageSize
    ) : IHttpRequest;
