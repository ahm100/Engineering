
using Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetFilteredWarehousesByGroupId;

namespace Engineering.Application.WebServices.WarehouseServices.Groups.Queries.GetFilteredWarehousesByGroupId;

public record GetFilteredWarehousesByGroupIdQuery(
    List<long> WarehouseIds,
    long GroupId,
    string? FilterData,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<GetFilteredWarehousesByGroupIdModel>>>;

