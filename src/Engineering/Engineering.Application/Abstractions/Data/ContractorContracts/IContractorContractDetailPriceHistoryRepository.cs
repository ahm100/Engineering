using Engineering.Application.Services.ContractorContracts.Contracts.GetContractorPriceHistory;
using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Abstractions.Data.ContractorContracts;

public interface IContractorContractDetailPriceHistoryRepository : IBaseRepository<ContractorContractDetailPriceHistory>
{
    Task<(List<GetContractorPriceHistoryModel> Data, int RowCount)> GetContractorPriceHistory(
        long id,
        int pageIndex,
        int pageSize,
        CT ct);
}
