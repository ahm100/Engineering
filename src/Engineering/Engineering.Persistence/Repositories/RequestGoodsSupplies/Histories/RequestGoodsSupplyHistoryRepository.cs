using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Histories;

namespace Engineering.Persistence.Repositories.RequestGoodsSupplies.Histories;

public class RequestGoodsSupplyHistoryRepository : BaseRepository<EngineeringDBContext, RequestGoodsSupplyHistory>, IRequestGoodsSupplyHistoryRepository
{
    public RequestGoodsSupplyHistoryRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<(List<RequestGoodsSupplyHistory> Data, int RowCount)> GetByRequestGoodsSupplyId(long id, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet.Include(oo => oo.RequestGoodsSupply)
                         .Where(c => c.RequestGoodsSupply.Id.Equals(id));


        query = query.OrderByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }
}
