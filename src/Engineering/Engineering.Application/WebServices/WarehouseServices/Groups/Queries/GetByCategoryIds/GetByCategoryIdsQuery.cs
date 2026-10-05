using Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetByCategoryIds;

namespace Engineering.Application.WebServices.WarehouseServices.Groups.Queries.GetByCategoryIds;

public record GetGroupsByCategoryIdsQuery(
    List<long> CategoryIds,
    int PageIndex,
    int PageSize) : IQuery<DataResult<List<GetFilteredGroupsModel>>>;