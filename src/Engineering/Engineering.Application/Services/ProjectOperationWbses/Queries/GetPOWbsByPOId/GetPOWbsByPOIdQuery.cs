using Engineering.Application.Services.ProjectOperationWbses.Contracts.GetPOWbsByPOId;

namespace Engineering.Application.Services.ProjectOperationWbses.Queries.GetPOWbsByPOId;

public record GetPOWbsByPOIdQuery(
    long ProjectOperationId,
    int PageIndex,
    int PageSize) : IQuery<GetPOWbsByPOIdResponse?>;