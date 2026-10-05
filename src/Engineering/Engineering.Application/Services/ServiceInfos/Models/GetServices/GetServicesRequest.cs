namespace Engineering.Application.Services.ServiceInfos.Models.GetServices;

public record GetServiceInfosRequest(
    long? OperationInfoId,
    string? FilterData,
    string? ServiceInfoCode,
    string? ServiceInfoName,
    long? CategoryId,
    long? BranchId,
    long? SeasonId,
    bool? IsActive,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
