using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetFilteredContractorContractHistory;

public record GetContractorContractHistoryQuery(
    long ContractorContractId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<ContractorContractHistory>>>;