using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Abstractions.Data.ContractorContracts;

public interface IContractorContractHistoryRepository : IBaseRepository<ContractorContractHistory>
{
    Task<(List<ContractorContractHistory> Data, int RowCount)> GetFilteredContractorContractHistory(long contractorContractId,
                                                                                                    int pageIndex,
                                                                                                    int pageSize,
                                                                                                    CT ct);
}
