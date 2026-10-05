using Engineering.Domain.Entities.RequestGoodsSupplies.Histories;

namespace Engineering.Application.Abstractions.Data.RequestGoodsSupplies;

public interface IRequestGoodsSupplyProductHistoryRepository : IBaseRepository<RequestGoodsSupplyProductHistory>
{
    Task<(List<RequestGoodsSupplyProductHistory> Data, int RowCount)> GetGoodsSupplyProductHistoryById(long id,
        int pageIndex,
        int pageSize,
        CT ct);
}
