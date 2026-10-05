using Engineering.Application.Abstractions.Data.MetaEntities;
using Engineering.Domain.Entities.Synonyms.Warehouse.Warehouses;

namespace Engineering.Persistence.Repositories.Synonyms.MetaEntities;

public class WarehouseRepository : BaseRepository<EngineeringDBContext, ViewWarehouse>, IViewWarehouseRepository
{
    public WarehouseRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<ViewWarehouse?> GetWarehouseById(
        long id,
        CT ct)
    {
        return await DbSet.FirstOrDefaultAsync(x => x.Id == id, ct); ;
    }


    public async Task<List<ViewWarehouse>> GetByIds(
        List<long> ids,
        CT ct)
    {
        var query = DbSet.Where(oo => ids.Contains(oo.Id));

        return await query.ToListAsync(ct);
    }
}