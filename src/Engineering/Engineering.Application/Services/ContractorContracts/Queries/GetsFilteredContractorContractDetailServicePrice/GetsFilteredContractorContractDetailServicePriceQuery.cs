using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetsFilteredContractorContractDetailServicePrice;

public record GetsFilteredContractorContractDetailServicePriceQuery(
    long? ProjectOperationServiceId,
    long? ServiceInfoId,
    DateTime? StratDate,
    DateTime? EndDate,
    long? ContractorId,
    long? CostCenterId,
    long? ProjectId,
    string? FilterData,
    long? CompanyId,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<ContractorContractDetailPrice>>>;
