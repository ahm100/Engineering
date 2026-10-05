using Engineering.Application.Abstractions.Data.CostCenters;
using Engineering.Domain.Entities.CostCenters;

namespace Engineering.Persistence.Repositories.CostCenters;

public class CostCenterVirtualGroupRepository : BaseRepository<EngineeringDBContext, CostCenterVirtualGroup>, ICostCenterVirtualGroupRepository
{
    public CostCenterVirtualGroupRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<(List<CostCenterVirtualGroup> Data, int RowCount)> GetByCostCenterAsync(long costCenterId, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet.Include(oo => oo.Admins.Where(c => !c.IsDeleted))
                         .Where(oo => oo.CostCenter.Id.Equals(costCenterId));

        query = query.OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var entities = await query.ToListAsync(ct);

        return (entities, count);
    }
}
