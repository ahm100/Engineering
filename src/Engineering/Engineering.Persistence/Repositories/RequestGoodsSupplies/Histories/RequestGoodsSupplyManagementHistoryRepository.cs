using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Histories;

namespace Engineering.Persistence.Repositories.RequestGoodsSupplies.Histories;

public class RequestGoodsSupplyManagementHistoryRepository : BaseRepository<EngineeringDBContext, RequestGoodsSupplyManagementHistory>, IRequestGoodsSupplyManagementHistoryRepository
{
    public RequestGoodsSupplyManagementHistoryRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<(List<RequestGoodsSupplyManagementHistory> Data, int RowCount)> GetByRequestGoodsSupplyManagementId(long id, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet.Include(oo => oo.RequestGoodsSupplyManagement.RequestGoodsSupplyProduct!.RequestGoodsSupply)
                         .Where(c => c.RequestGoodsSupplyManagement.Id.Equals(id));


        query = query.OrderByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }
}
