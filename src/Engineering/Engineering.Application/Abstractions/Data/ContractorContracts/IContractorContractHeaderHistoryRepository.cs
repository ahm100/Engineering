using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Abstractions.Data.ContractorContracts;

public interface IContractorContractHeaderHistoryRepository : IBaseRepository<ContractorContractHeaderHistory>
{
    Task<(List<ContractorContractHeaderHistory> Data, int RowCount)> GetsContractorContractHeaderHistory(long contractorContractId,
                                                                                                    int pageIndex,
                                                                                                    int pageSize,
                                                                                                    CT ct);
}
