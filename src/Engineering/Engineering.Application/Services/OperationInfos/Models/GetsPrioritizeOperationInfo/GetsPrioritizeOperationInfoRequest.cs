namespace Engineering.Application.Services.OperationInfos.Models.GetsPrioritizeOperationInfo;

public record GetsPrioritizeOperationInfoRequest(
    string? FilterData,
    long? Id,
    int Priority,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
