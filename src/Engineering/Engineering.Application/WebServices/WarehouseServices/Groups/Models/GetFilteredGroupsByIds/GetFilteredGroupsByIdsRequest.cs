namespace Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetFilteredGroupsByIds;

public record GetFilteredGroupsByIdsRequest(List<long> Ids, string? FilterData, string? ProductFilterData, int PageIndex, int PageSize);