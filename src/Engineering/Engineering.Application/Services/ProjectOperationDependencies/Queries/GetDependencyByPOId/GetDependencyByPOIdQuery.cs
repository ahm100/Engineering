using Engineering.Application.Services.ProjectOperationDependencies.Contracts.GetDependencyByPOId;

namespace Engineering.Application.Services.ProjectOperationDependencies.Queries.GetDependencyByPOId;

public record GetDependencyByPOIdQuery(
    long ProjectOperationId,
    int PageIndex,
    int PageSize
    ) : IQuery<GetDependencyByPOIdResponse?>;