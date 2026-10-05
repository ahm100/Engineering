using Engineering.Application.Services.TransportationContractors.Contracts.GetsPriceWeightHistory;
using Engineering.Domain.Entities.Logistics;

namespace Engineering.Application.Abstractions.Data;

public interface ITransportationContractorPriceWeightHistoryRepository : IBaseRepository<TransportationContractorPriceWeightHistory>
{
    Task<(List<GetsPriceWeightHistoryResponseModel> Data, int RowCount)> GetsPriceWeightHistory(
        long id,
        int pageIndex,
        int pageSize,
        CT ct);
}