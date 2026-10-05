using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Domain.Entities.GoodsManager;

namespace Engineering.Persistence.Repositories.RequestGoodsSupplies;

public class GoodsManagerAssignmentHistoryRepository
    : BaseRepository<EngineeringDBContext, GoodsManagerAssignmentHistory>,
      IGoodsManagerAssignmentHistoryRepository
{
    public GoodsManagerAssignmentHistoryRepository(EngineeringDBContext context)
        : base(context) { }

    public async Task<(List<GoodsManagerAssignmentHistory> Data, int RowCount)> GetByAssignmentId(
        long assignmentId,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        // ⬇️ Fixed: Explicitly type as IQueryable to allow .Page() reassignment
        IQueryable<GoodsManagerAssignmentHistory> query = DbSet
            .Where(x => x.GoodsManagerAssignmentId == assignmentId)
            .OrderByDescending(x => x.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 && pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }
}