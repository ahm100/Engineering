using Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetActiveGroups;

namespace Engineering.Application.WebServices.WarehouseServices.Groups.Queries.GetActiveGroups;

public record GetActiveGroupsQuery(string? FilterData, int PageIndex, int PageSize) : IQuery<DataResult<List<ActiveGroupsModel>>>;