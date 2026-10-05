using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetsContractorContractHeaderHistory;

public record GetsContractorContractHeaderHistoryQuery(
    long ContractorContractId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<ContractorContractHeaderHistory>>>;