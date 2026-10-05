namespace Engineering.Application.Services.ProjectOperationDependencies.Contracts.GetDependencyByPOId;

public record GetDependencyByPOIdRequest(
    long ProjectOperationId,
    int PageIndex,
    int PageSize) : IHttpRequest;