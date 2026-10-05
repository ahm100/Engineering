namespace Engineering.Application.Services.OperationInfos.Models.GetOperationInfos;

public record GetOperationInfosRequest(
    string? FilterData,
    long? CategoryId,
    long? BranchId,
    long? SeasonId,
    long? DependencyId,
    bool? IsActive,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
