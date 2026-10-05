
namespace Engineering.Application.WebServices.WarehouseServices.Warehouse.Models.GetWarehouseByIds;

public record GetWarehouseByIdsQuery(List<long> Ids, bool? IgnoreQuery, int PageIndex, int PageSize
) : IQuery<DataResult<List<GetWarehouseByIdsModel>>>;
