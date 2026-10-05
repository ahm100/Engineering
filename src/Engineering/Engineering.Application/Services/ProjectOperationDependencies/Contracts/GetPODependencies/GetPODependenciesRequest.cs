namespace Engineering.Application.Services.ProjectOperationDependencies.Contracts.GetPODependencies;

public record GetPODependenciesRequest(
    long ProjectOperationId) : IHttpRequest;