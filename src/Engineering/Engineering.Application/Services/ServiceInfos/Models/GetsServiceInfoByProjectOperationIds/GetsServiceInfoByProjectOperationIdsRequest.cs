namespace Engineering.Application.Services.ServiceInfos.Models.GetsServiceInfoByProjectOperationIds;

public record GetsServiceInfoByProjectOperationIdsRequest(
    List<long>? ProjectOperationIds,
    string? FilterData,
    bool? IsActive,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
