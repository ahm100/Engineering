
namespace Engineering.Application.Services.EngineeringDocs.Contracts.UpdateProjectDoc;

public record UpdateProjectDocRequest(
    long Id,
    long DisciplineId,
    long DisciplineDocId,
    long ThirdPartyId,
    string Url
) : IHttpRequest;