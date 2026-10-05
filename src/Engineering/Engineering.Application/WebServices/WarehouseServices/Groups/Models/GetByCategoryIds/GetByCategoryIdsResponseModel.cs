namespace Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetByCategoryIds;

public record GetByCategoryIdsResponseModel(
    List<GetFilteredGroupsModel> Data,
    int RowCount
    );