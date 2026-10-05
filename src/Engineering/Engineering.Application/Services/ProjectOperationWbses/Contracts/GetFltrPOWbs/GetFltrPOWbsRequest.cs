namespace Engineering.Application.Services.ProjectOperationWbses.Contracts.GetFltrPOWbs;

public record GetFltrPOWbsRequest(
    long? ProjectId,
    List<long>? ProjectOperationIds,
    List<long>? ProjectWbsIds,
    List<long>? ProjectOperationWbsIds,
    string? FilterData,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;