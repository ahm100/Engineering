namespace Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetProductGroupByWareHouseId;

public record GetProductGroupByWareHouseIdRequest(List<long> WareHouseIds, List<long>? CategoryIds, int PageIndex, int PageSize);