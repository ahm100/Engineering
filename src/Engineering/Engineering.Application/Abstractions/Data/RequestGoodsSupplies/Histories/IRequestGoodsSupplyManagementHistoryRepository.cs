using Engineering.Domain.Entities.RequestGoodsSupplies.Histories;

namespace Engineering.Application.Abstractions.Data.RequestGoodsSupplies;

public interface IRequestGoodsSupplyManagementHistoryRepository : IBaseRepository<RequestGoodsSupplyManagementHistory>
{
    Task<(List<RequestGoodsSupplyManagementHistory> Data, int RowCount)> GetByRequestGoodsSupplyManagementId(long id,
        int pageIndex,
        int pageSize,
        CT ct);
}
