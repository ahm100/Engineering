

namespace Engineering.Application.Services.EngineeringDocs.Contracts.DeleteProjectDoc;

public record DeleteProjectDocRequest(
    long Id) : IHttpRequest;