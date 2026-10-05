
namespace Engineering.Application.Services.ContractorContracts.Contracts.GetsPartialProjectOperationDetailService;

public record GetsPartialProjectOperationDetailServiceRequest(
    long? CostCenterId,
    long? ProjectId,
    long? ContractorId,
    long ServiceInfoId,
    List<long>? ProjectOperationDetailServiceIds,
    string? FilterData
    ) : IHttpRequest;
