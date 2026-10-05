
using Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetFilteredWarehousesByGroupIds;

namespace Engineering.Application.WebServices.WarehouseServices.Groups.Queries.GetFilteredWarehousesByGroupIds;

public record GetFilteredWarehousesByGroupIdsQuery(
    List<long> WarehouseIds,
    List<long> GroupIds,
    string? FilterData,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<GetFilteredWarehousesByGroupIdsModel>>>;

