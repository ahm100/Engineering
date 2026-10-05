namespace Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetFilteredWarehousesByGroupId;

public record GetFilteredWarehousesByGroupIdModel(
    long? Id,
    long? ManagerId,
    string? ManagerFullName,
    string? Name,
    string? Code,
    bool? IsMain,
    bool? IsReference
    );
