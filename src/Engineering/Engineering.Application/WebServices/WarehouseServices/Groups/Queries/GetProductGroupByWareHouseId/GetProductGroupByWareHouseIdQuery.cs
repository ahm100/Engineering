using Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetProductGroupByWareHouseId;

namespace Engineering.Application.WebServices.WarehouseServices.Groups.Queries.GetActiveGroups;

public record GetProductGroupByWareHouseIdQuery(
    List<long> WareHouseIds,
    List<long>? CategoryIds,
    int PageIndex,
    int PageSize) : IQuery<DataResult<List<GroupsModel>>>;