using Engineering.Application.Services.ContractorContracts.Contracts.GetsPartialProjectOperationDetailService;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetsPartialProjectOperationDetailService;

public record GetsPartialProjectOperationDetailServiceQuery(
    long? CostCenterId,
    long? ProjectId,
    long? ContractorId,
    long ServiceInfoId,
    List<long>? ProjectOperationDetailServiceIds,
    string? FilterData,
    long? CompanyId
    ) : IQuery<DataResult<List<GetsPartialProjectOperationDetailServiceModel>>>;
