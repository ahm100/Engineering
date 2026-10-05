using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetFilteredContractorContractDetails;

public record GetFilteredContractorContractDetailsQuery(
    long? ProjectOperationServiceId,
    long? ProjectOperationId,
    DateTime? StratDate,
    DateTime? EndDate,
    long? ContractorId,
    string? FilterData,
    long? CompanyId,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<ContractorContractDetail>>>;
