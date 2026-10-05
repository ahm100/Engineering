namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailByContractorIds;

public record GetsProjectOperationDetailByContractorIdsRequest(
    List<long> ContractorIds,
    List<long>? CostCenterIds,
    List<long>? ProjectIds,
    List<long>? OperationInfoIds,
    List<long>? ProjectOperationIds,
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
