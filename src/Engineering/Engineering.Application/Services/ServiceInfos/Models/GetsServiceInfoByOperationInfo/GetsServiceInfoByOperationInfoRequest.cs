namespace Engineering.Application.Services.ServiceInfos.Models.GetsServiceInfoByOperationInfo;

public record GetsServiceInfoByOperationInfoRequest(
    List<long>? OperationInfoIds,
    string? FilterData,
    bool? IsActive,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
