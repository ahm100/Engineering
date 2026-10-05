using Engineering.Application.Services.ShippingCosts.Contracts.GetShppingCostHistory;
using Engineering.Domain.Entities.Logistics;

namespace Engineering.Application.Abstractions.Data;

public interface IShippingCostHistoryRepository : IBaseRepository<ShippingCostHistory>
{
    Task<(List<GetShippingCostHistoryResponseModel> Data, int RowCount)> GetShippingCostHistory(
        long id,
        int pageIndex,
        int pageSize,
        CT ct);
}