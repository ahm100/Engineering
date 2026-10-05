using Engineering.Application.Services.ProjectOperationWbses.Contracts.GetFltrPOWbs;

namespace Engineering.Application.Services.ProjectOperationWbses.Queries.GetFltrPOWbs;

public record GetFltrPOWbsQuery(
    long? ProjectId,
    List<long>? ProjectOperationIds,
    List<long>? ProjectWbsIds,
    List<long>? ProjectOperationWbsIds,
    string? FilterData,
    int PageIndex,
    int PageSize
    ) : IQuery<GetFltrPOWbsResponse?>;