using Engineering.Application.Abstractions.Data.CostCenters;
using CostCenterInformedUser = Engineering.Domain.Entities.CostCenters.CostCenterInformedUser;

namespace Engineering.Persistence.Repositories.CostCenters;

public class CostCenterInformedUserRepository : BaseRepository<EngineeringDBContext, CostCenterInformedUser>, ICostCenterInformedUserRepository
{
    public CostCenterInformedUserRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<(List<CostCenterInformedUser> Data, int RowCount)> GetCostCenterInformedUserByCostCenter(long costCenterId, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet.Where(oo => oo.CostCenter.Id == costCenterId);

        query = query.OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var entities = await query.ToListAsync(ct);

        return (entities, count);
    }

    public async Task<(List<CostCenterInformedUser> Data, int RowCount)> GetInformedUsersByCostCenter(long costCenterId, CT ct)
    {
        var query = DbSet
            .Where(oo => oo.CostCenter.Id == costCenterId)
            .OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);
        var entities = await query
            .ToListAsync(ct);

        return (entities, count);
    }

    public async Task<CostCenterInformedUser?> FindUser(long userId, long costCenterId, CT ct)
    {
        var query = DbSet
            .Where(oo => oo.EmployeeId == userId && oo.CostCenter.Id == costCenterId);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }
}