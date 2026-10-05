using Engineering.Application.Abstractions.Data.CostCenters;
using CostCenterWarehouse = Engineering.Domain.Entities.CostCenters.CostCenterWarehouse;

namespace Engineering.Persistence.Repositories.CostCenters;

public class CostCenterWarehouseRepository : BaseRepository<EngineeringDBContext, CostCenterWarehouse>, ICostCenterWarehouseRepository
{
    public CostCenterWarehouseRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<CostCenterWarehouse?> FindCostCenterWarehouse(long id, CT ct)
    {
        var query = DbSet
            .Where(oo => oo.Id == id)
            .Include(x => x.CostCenter);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<CostCenterWarehouse?> FindForUpdate(long id, CT ct)
    {
        var query = DbSet
            .Where(oo => oo.Id == id)
            .Include(x => x.CostCenter)
                .ThenInclude(c => c.CostCenterWarehouses);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<(List<CostCenterWarehouse> Data, int RowCount)> GetsCostCenterWarehouseByCostCenter(long costCenterId, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet.Include(x => x.CostCenter)
            .Where(oo => oo.CostCenter.Id == costCenterId);

        query = query.OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var entities = await query.ToListAsync(ct);
        return (entities, count);
    }

    public async Task<(List<CostCenterWarehouse> Data, int RowCount)> GetsCostCenterWarehouseByCostCenterIds(List<long> costCenterIds, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet

            .Include(x => x.CostCenter)

            .Where(oo => costCenterIds.Contains(oo.CostCenter.Id));

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var entities = await query.ToListAsync(ct);
        return (entities, count);
    }

    public async Task<(List<CostCenterWarehouse> Data, int RowCount)> GetsCostCenterWarehouseInventory(long costCenterId, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet
            .Where(oo => oo.CostCenter.Id == costCenterId);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var entities = await query.ToListAsync(ct);
        return (entities, count);
    }

    public async Task<(List<CostCenterWarehouse> Data, int RowCount)> GetsCostCenterWarehouseForSupplyManagement(CT ct)
    {
        var query = DbSet.Include(oo => oo.CostCenter)
            ;

        var count = await query.CountAsync(ct);
        var entities = await query
            .ToListAsync(ct);

        return (entities, count);
    }

    public async Task<List<CostCenterWarehouse>> GetByWarehouseIds(List<long> warehouseIds, CT ct)
    {
        var query = DbSet.Include(oo => oo.CostCenter).Where(oo => warehouseIds.Contains(oo.WarehouseId));
        return await query.ToListAsync(ct);
    }
    public async Task<List<CostCenterWarehouse>> GetDefaultByCostCenterIds(List<long> costCenterIds, CT ct)
    {
        if (costCenterIds == null) throw new ArgumentNullException(nameof(costCenterIds));
        var query = DbSet.Include(oo => oo.CostCenter)
            .Where(oo => costCenterIds.Contains(oo.CostCenter.Id) && oo.IsDefault);
        return await query.ToListAsync(ct);
    }

    public async Task<CostCenterWarehouse?> GetDefaultByCostCenterId(long costCenterId, CT ct)
    {
        var query = DbSet.Include(oo => oo.CostCenter)
                          .Where(oo => oo.CostCenter.Id.Equals(costCenterId) && oo.IsDefault);
        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<List<CostCenterWarehouse>?> FindWarehouseByCostCenter(long id, CT ct)
    {
        var query = DbSet
            .Include(x => x.CostCenter)
            .Where(oo => oo.CostCenterId == id);

        var result = await query.ToListAsync(ct);
        return result;
    }

    public async Task<List<long>?> GetWarehouseIds(
        long costCenterId, CT ct)
    {
        return await DbSet
            .Where(oo => oo.CostCenterId == costCenterId)
            .Select(x => x.WarehouseId).ToListAsync(ct);
    }
}