namespace Engineering.Application.Services.EngineeringDocs.Contracts.GetProjectDocById;

public record GetProjectDocByIdRequest(
    long Id) : IHttpRequest;