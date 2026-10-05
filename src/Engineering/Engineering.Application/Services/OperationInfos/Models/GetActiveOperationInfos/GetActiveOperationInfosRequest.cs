namespace Engineering.Application.Services.OperationInfos.Models.GetActiveOperationInfos;

public record GetActiveOperationInfosRequest(
    string? FilterData,
    long? CategoryId,
    long? BranchId,
    long? SeasonId,
    int? Priority,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
