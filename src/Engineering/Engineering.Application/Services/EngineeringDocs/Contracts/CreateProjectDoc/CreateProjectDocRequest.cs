
namespace Engineering.Application.Services.EngineeringDocs.Contracts.CreateProjectDoc;

public record CreateProjectDocRequest(
    long ProjectId,
    long DisciplineId,
    long DisciplineDocId,
    long ThirdPartyId,
    string Url,
    string Description
) : IHttpRequest;