using Engineering.Application.Services.ProjectOperationDependencies.Contracts.GetFltrDependency;

namespace Engineering.Application.Services.ProjectOperationDependencies.Queries.GetFltrDependency;

public record GetFltrDependencyQuery(
    List<long>? ProjectOperationIds,
    string? FilterData,
    int PageIndex,
    int PageSize
    ) : IQuery<GetFltrDependencyResponse?>;