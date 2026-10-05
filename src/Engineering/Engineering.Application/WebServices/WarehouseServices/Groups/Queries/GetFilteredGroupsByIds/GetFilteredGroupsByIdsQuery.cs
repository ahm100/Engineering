using Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetFilteredGroupsByIds;

namespace Engineering.Application.WebServices.WarehouseServices.Groups.Queries.GetFilteredGroupsByIds;

public record GetFilteredGroupsByIdsQuery(List<long> Ids, string? FilterData, string? ProductFilterData, int PageIndex, int PageSize) : IQuery<DataResult<List<FilteredGroup>>>;