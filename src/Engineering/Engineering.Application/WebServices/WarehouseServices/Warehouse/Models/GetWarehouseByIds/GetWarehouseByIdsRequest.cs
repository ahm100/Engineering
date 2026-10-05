namespace Engineering.Application.WebServices.WarehouseServices.Warehouse.Models.GetWarehouseByIds;

public record GetWarehouseByIdsRequest(List<long> Ids, bool? IgnoreQuery, int PageIndex, int PageSize);