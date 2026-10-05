using Engineering.Domain.Entities.RequestGoodsSupplies.Histories;

namespace Engineering.Application.Abstractions.Data.RequestGoodsSupplies;

public interface IRequestGoodsSupplyDetailHistoryRepository : IBaseRepository<RequestGoodsSupplyDetailHistory>
{
    Task<(List<RequestGoodsSupplyDetailHistory> Data, int RowCount)> GetByRequestGoodsSupplyDetailId(long id,
        int pageIndex,
        int pageSize,
        CT ct);
}
