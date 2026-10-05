using Engineering.Application.Services.ProjectOperationDependencies.Contracts.GetPODependencies;

namespace Engineering.Application.Services.ProjectOperationDependencies.Queries.GetPODependencies;

public record GetPODependenciesQuery(
    long ProjectOperationId) : IQuery<GetPODependenciesResponse?>;