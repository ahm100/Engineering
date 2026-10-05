namespace Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetFilteredWarehousesByGroupIds;

public record GetFilteredWarehousesByGroupIdsModel(
    long? Id,
    long? ManagerId,
    string? ManagerFullName,
    string? Name,
    string? Code,
    bool? IsMain,
    bool? IsReference
    );
