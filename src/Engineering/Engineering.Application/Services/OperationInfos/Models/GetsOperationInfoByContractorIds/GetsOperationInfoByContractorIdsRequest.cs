namespace Engineering.Application.Services.OperationInfos.Models.GetsOperationInfoByContractorIds;

public record GetsOperationInfoByContractorIdsRequest(
    List<long> ContractorIds,
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
