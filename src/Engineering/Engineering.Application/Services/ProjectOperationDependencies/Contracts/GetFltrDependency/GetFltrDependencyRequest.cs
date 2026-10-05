namespace Engineering.Application.Services.ProjectOperationDependencies.Contracts.GetFltrDependency;

public record GetFltrDependencyRequest(
    List<long>? ProjectOperationIds,
    string? FilterData,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;