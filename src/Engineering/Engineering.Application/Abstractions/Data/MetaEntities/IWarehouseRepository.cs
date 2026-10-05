using Engineering.Domain.Entities.Synonyms.Warehouse.Warehouses;

namespace Engineering.Application.Abstractions.Data.MetaEntities;

public interface IViewWarehouseRepository : IBaseRepository<ViewWarehouse>
{
    Task<ViewWarehouse?> GetWarehouseById(
          long id,
          CT ct);

    Task<List<ViewWarehouse>> GetByIds(
            List<long> ids,
            CT ct);
}