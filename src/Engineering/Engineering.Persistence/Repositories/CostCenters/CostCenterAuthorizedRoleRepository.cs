using Engineering.Application.Abstractions.Data.CostCenters;
using CostCenterAuthorizedRole = Engineering.Domain.Entities.CostCenters.CostCenterAuthorizedRole;

namespace Engineering.Persistence.Repositories.CostCenters;

public class CostCenterAuthorizedRoleRepository : BaseRepository<EngineeringDBContext, CostCenterAuthorizedRole>, ICostCenterAuthorizedRoleRepository
{
    public CostCenterAuthorizedRoleRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<(List<CostCenterAuthorizedRole> Data, int RowCount)> GetAuthorizedRolesByCostCenter(long costCenterId, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet
            .Where(oo => oo.CostCenter.Id == costCenterId);

        query = query.OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var entities = await query.ToListAsync(ct);

        return (entities, count);
    }

    public async Task<(List<CostCenterAuthorizedRole> Data, int RowCount)> GetAuthorizedRolesByCostCenter(long costCenterId, CT ct)
    {
        var query = DbSet
            .Where(oo => oo.CostCenter.Id == costCenterId)
            .OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);
        var entities = await query
            .ToListAsync(ct);

        return (entities, count);
    }

    public async Task<CostCenterAuthorizedRole?> FindRole(long roleId, long costCenterId, CT ct)
    {
        var query = DbSet
            .Where(oo => oo.AuthorizedRoleId == roleId && oo.CostCenter.Id == costCenterId)
            .OrderByDescending(oo => oo.Created);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }
}