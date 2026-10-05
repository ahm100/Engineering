using Engineering.Domain.Entities.RequestGoodsSupplies.Histories;

namespace Engineering.Application.Abstractions.Data.RequestGoodsSupplies;

public interface IRequestGoodsSupplyHistoryRepository : IBaseRepository<RequestGoodsSupplyHistory>
{
    Task<(List<RequestGoodsSupplyHistory> Data, int RowCount)> GetByRequestGoodsSupplyId(long id,
        int pageIndex,
        int pageSize,
        CT ct);
}
