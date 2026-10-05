
namespace Engineering.Application.Services.EngineeringDocs.Contracts.GetDiscipline;

public record GetDisciplineRequest(
    int PageIndex,
    int PageSize
    ) : IHttpRequest;
