
namespace Engineering.Application.Services.ContractorContracts.Contracts.GetsIntegratedProjectOperationDetailService;

public record GetsIntegratedProjectOperationDetailServiceRequest(
    long? CostCenterId,
    long? ProjectId,
    long? ContractorId,
    List<long>? ProjectOperationIds,
    List<long>? ServiceInfoIds,
    string? FilterData,
    string[]? OrderBy,
    int PageIndex,
    int PageSize) : IHttpRequest;
