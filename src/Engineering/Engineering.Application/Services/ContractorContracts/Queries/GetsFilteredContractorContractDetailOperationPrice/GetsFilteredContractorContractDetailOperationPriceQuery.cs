using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetsFilteredContractorContractDetailOperationPrice;

public record GetsFilteredContractorContractDetailOperationPriceQuery(
    long? ProjectOperationId,
    long? OperationInfoId,
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
