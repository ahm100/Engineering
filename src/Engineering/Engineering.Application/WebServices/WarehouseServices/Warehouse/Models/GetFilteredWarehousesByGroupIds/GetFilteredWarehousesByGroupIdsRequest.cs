namespace Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetFilteredWarehousesByGroupIds;

public record GetFilteredWarehousesByGroupIdsRequest(
    List<long> WarehouseIds,
    List<long> GroupIds,
    string? FilterData,
    int PageIndex,
    int PageSize
    );
