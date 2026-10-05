namespace Engineering.Application.WebServices.WarehouseServices.Inventories.Models.GetProductInventoryByFilter;

public record GetProductInventoryByFilterRequest(
    long ProductId,
    List<long>? WareHouseIds,
    string? FilterData,
    int PageIndex,
    int PageSize
    );
