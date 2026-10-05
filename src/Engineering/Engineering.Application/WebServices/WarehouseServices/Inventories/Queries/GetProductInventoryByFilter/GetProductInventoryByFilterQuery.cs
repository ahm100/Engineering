using Engineering.Application.WebServices.WarehouseServices.Inventories.Models.GetProductInventoryByFilter;

namespace Engineering.Application.WebServices.WarehouseServices.Inventories.Queries.GetProductInventoryByFilter;

public record GetProductInventoryByFilterQuery(
    long ProductId,
    List<long>? WareHouseIds,
    string? FilterData,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<FilteredInventory>>>;
