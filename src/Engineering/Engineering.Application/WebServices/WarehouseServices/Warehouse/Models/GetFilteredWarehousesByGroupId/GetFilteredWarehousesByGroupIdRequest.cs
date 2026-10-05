namespace Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetFilteredWarehousesByGroupId;

public record GetFilteredWarehousesByGroupIdRequest(
    List<long> WarehouseIds,
    long GroupId,
    string? FilterData,
    int PageIndex,
    int PageSize
    );
