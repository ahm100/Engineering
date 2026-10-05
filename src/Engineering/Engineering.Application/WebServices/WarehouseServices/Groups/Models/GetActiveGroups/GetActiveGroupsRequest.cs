namespace Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetActiveGroups;

public record GetActiveGroupsRequest(string? FilterData, int PageIndex, int PageSize);