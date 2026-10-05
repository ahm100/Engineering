using Engineering.Application.Abstractions.Data.CostCenters;
using CostCenterAuthorizedUser = Engineering.Domain.Entities.CostCenters.CostCenterAuthorizedUser;

namespace Engineering.Persistence.Repositories.CostCenters;

public class CostCenterAuthorizedUserRepository : BaseRepository<EngineeringDBContext, CostCenterAuthorizedUser>, ICostCenterAuthorizedUserRepository
{
    public CostCenterAuthorizedUserRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<(List<CostCenterAuthorizedUser> Data, int RowCount)> GetAuthorizedUsersByCostCenter(long costCenterId, int pageIndex, int pageSize, CT ct)
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

    public async Task<(List<CostCenterAuthorizedUser> Data, int RowCount)> GetAuthorizedUsersByCostCenter(long costCenterId, CT ct)
    {
        var query = DbSet
            .Where(oo => oo.CostCenter.Id == costCenterId)
            .OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);
        var entities = await query
            .ToListAsync(ct);

        return (entities, count);
    }

    public async Task<CostCenterAuthorizedUser?> FindUser(long userId, long costCenterId, CT ct)
    {
        var query = DbSet
            .Where(oo => oo.AuthorizedUserId == userId && oo.CostCenter.Id == costCenterId)
            .OrderByDescending(oo => oo.Created);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }
}